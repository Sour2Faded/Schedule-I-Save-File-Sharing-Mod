# Schedule-I-Save-File-Sharing-Mod
A file sharing mod so you can share and play on friends saves without needing them to be online!


# How To Setup and use the mod

## 1. Get A Supabase project
	-Goto: https://supabase.com/ and create an account
	-Goto the Supabase dashboard and create a new project
 ![SupaBaseExample](Tut pics/SupaBaseExample.png)
	(Note: Make sure you save your projects ID this is located at the end of the link when you are on the main page EX:supabase.com/dashboard/project/ThisIsWhereUrIDIs)
## 2. Create a new bucket
	-Goto the storage tab on the left hand side
	-Press the "Create bucket" button 
	-Make your bucket PUBLIC 
 ![NewBucket](Tut pics/NewBucket.png)
 ![MakeSureThisIsPUBLIC](Tut pics/MakeSureThisIsPUBLIC.png)
## 3. Upload the save folder to the bucket
	-Find your save folder and upload it (saves are located at: C:\Users\[NAME]\AppData\LocalLow\TVGS\Schedule I\Saves\[ID])
![DummyOrSaveFile](Tut pics/DummyOrSaveFile.png)
## 4. Get your API Key
	-Goto project settings on the left hand side
	-Goto the Data API tab of project settings
	-Save your "service_role" id
![DummyOrSaveFile](Tut pics/CopyYourAPIKey.png)
## 5. Set up your config file
	-Launch your game with the mod installed to make the empty config file (Located at: ...SteamLibrary\steamapps\common\Schedule I\UserData\SaveFileSharing.cfg)
	-Replace [ID] in the links with your Suprabase project ID 
	-Input your API Key
	-Find your Steam ID by going to Steam and pressing your Steam name in the top right then "Account Details" and your Steam ID shows below your username
	-Input your Steam ID(this is for finding the save path and changing the save before the game loads them)

Now when launching the game you will automaticlly download the uploaded save file from Supabase and when quitting the game you will automaticlly upload the current save file to the Supabase storage, all your fiends need to do is have the mod installed and the same config as you(Besides the steam ID) and you will be sharing the save file with them!
