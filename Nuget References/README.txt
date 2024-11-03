Sure, Here's a basic guide to get you started. This guide assumes you have the Visual Studio SDK installed. If not, you can download it from the Visual Studio Marketplace.

1. Create a new VSIX Project
   - Open Visual Studio and create a new project.
   - From the project templates, choose "VSIX Project".
   - Name your project and click "Create".

2. Add a Custom Command
   - Right-click on your project in the Solution Explorer and choose "Add" -> "New Item".
   - From the Add New Item dialog, choose "Custom Command" and name your command.

3. Modify the .vsct file
   - In the newly added Command file, you'll see a .vsct file. This file is used to define the UI of your command.
   - You'll need to modify the following sections:
     - `<Symbols>` - This is where you define the ID and text of your command.
     - `<Button>` - This is where you set your command's parameters like its parent (to make it a context menu item), icon, etc.
   - For a context menu on the NuGet package, you should set the parent of your command to be the context menu of the Solution Explorer. This can be done by setting the value of the `Parent` attribute in the `<Button>` element to `IDM_VS_CTXT_REFERENCEROOT`.

4. Write the execution logic
   - In the automatically generated command class (Command.cs by default), you can write the logic for what happens when your command is clicked in the `MenuItemCallback` method.
   - To show a message box with the name of the NuGet package, you could use the `VsShellUtilities.ShowMessageBox` method.

Here is a simple example of how you can implement the `MenuItemCallback` method:

```csharp
private void MenuItemCallback(object sender, EventArgs e)
{
    ThreadHelper.ThrowIfNotOnUIThread();

    IVsHierarchy hierarchyItem;
    uint itemid;
    if (!IsSingleProjectItemSelection(out hierarchyItem, out itemid)) return;

    // Get the project from the selected item
    IVsProject vsProject = (IVsProject)hierarchyItem;
    vsProject.GetMkDocument(itemid, out string itemFullPath);

    // Get the project name
    var projectName = Path.GetFileNameWithoutExtension(itemFullPath);

    // Show a message box to prove we were here
    VsShellUtilities.ShowMessageBox(
        this.package,
        "Nuget Package Name: " + projectName,
        "My Command",
        OLEMSGICON.OLEMSGICON_INFO,
        OLEMSGBUTTON.OLEMSGBUTTON_OK,
        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
}
```

Remember, this is just a basic starting point. You'll need to adapt this to fit your needs, and most importantly, you'll need to implement the logic to actually get the NuGet package references. You might need to use the `EnvDTE` or `EnvDTE80` interfaces and their `GetReferences()` method to get the references of the project.