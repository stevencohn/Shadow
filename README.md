In Windows 10, you can pin any file or executable to the Start menu.
With files, such as documents and spreadsheets, the icon that is associated
with the Start menu tile is gleened from the application used to open the file.
So with a spreadsheet, you'll see the Excel icon.

This tiny application lets you associate a different icon with a file by acting
as a proxy application - with its own icon - that redirects to the target file.

So this accomplishes two things:

1. It provides a specific icon that is visible to the Windows 10 start menu.
   The start menu by default will skip any icon set for a shortcut and instead
   use the default icon of the underlying executable.  This avoids that by
   binding to the default icon of this executable instead.
2. It avoids opening and displaying a command prompt window, as would be the
   case for a console application, by instead posing as a WinForm application
   even though we don't actually instantiate a windows form.

 To use:

* Create a shortcut in the start menu folder, such as
  %AppData%\Microsoft\Windows\Start Menu\Programs\Shadow.lnk
* Change the target appSetting value in app.config to the location of
  file or executable you want to invoke. Use "%" to delim an environment
  variable name or "$" to delim a special folder name, for example
  <add key="target" value="$UserProfile$\Briefcase\Shadow.xlsx"/>     
* It should appear in the Windows 10 start menu now.

Don't like the icon? Change it!

* You can change the icon by replacing it in the source code and recompiling.
* Of course this means that you can have only one icon for a single instance 
  of this application.
* If you want multiple instances with different icons, just recompile and 
  rename the executable using any appropriate name fort the target file.