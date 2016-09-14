The following obsolete properties of the GridViewCommandColumn class have been removed in version 15.1:
NewButton
EditButton
DeleteButton
UpdateButton
CancelButton
SelectButton
ClearFilterButton
 
Now, the visibility of command buttons can be specified using the following properties of the GridViewCommandColumn class:
ShowNewButton
ShowEditButton
ShowDeleteButton
ShowUpdateButton
ShowCancelButton
ShowSelectButton
ShowClearFilterButton
 
Other button settings can be defined using the properties of the SettingsCommandButton class:
NewButton
EditButton
DeleteButton
UpdateButton
CancelButton
SelectButton
ClearFilterButton
 
The BC2948_CommandButtonsUpdater.exe application patches aspx markup files (.aspx, .ascx, .master, .skin) and correctly replaces obsolete properties with actual properties given in the above lists.
The tool analyzes all files in the specified folder and its subfolders. 
Before making replacements, the tool creates a backup file (with the *.dxbak extension) for each processed file.