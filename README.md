# Schedule-I-Save-File-Sharing-Mod
A file sharing mod so you can share and play on friends saves without needing them to be online!


# How To Setup and use the mod

## 1. Get A Supabase project

	-Goto: https://supabase.com/ and create an account
	-Goto the Supabase dashboard and create a new project
	(Note: Make sure you save your projects ID this is located at the end of the link when you are on the main page EX:supabase.com/dashboard/project/ThisIsWhereUrIDIs)
![SupaBaseExample](https://github.com/user-attachments/assets/4603a1a6-65ec-4509-8bfe-ab5bc24632e4)
	
## 2. Create a new bucket
	-Goto the storage tab on the left hand side
	-Press the "Create bucket" button 
![NewBucket](https://github.com/user-attachments/assets/ee447781-f2a3-42d4-9c67-5aeb5ad9fbaf)	
 	
  	-Make your bucket PUBLIC 

 ![MakeSureThisIsPUBLIC](https://github.com/user-attachments/assets/ba28488a-f122-411a-8d9b-6908ff3fb953)

## 3. Upload the save folder to the bucket
	-Find your save folder and upload it (saves are located at: C:\Users\[NAME]\AppData\LocalLow\TVGS\Schedule I\Saves\[SteamID])
![DummyOrSaveFile](https://github.com/user-attachments/assets/490487dd-1c3c-4fef-a901-49c39cf64d3e)

## 4. Get your API Key
	-Goto project settings on the left hand side
	-Goto the Data API tab of project settings
	-Save your "service_role" id
![CopyYourAPIKey](https://github.com/user-attachments/assets/66cb0df6-4df1-4a86-9745-d36a923cf32c)

## 5. Set up your config file
	-Launch your game with the mod installed to make the empty config file (Located at: ...SteamLibrary\steamapps\common\Schedule I\UserData\SaveFileSharing.cfg)
	-Replace [ID] in the links with your Suprabase project ID 
	-Input your API Key
	-Find your Steam ID by going to Steam and pressing your Steam name in the top right then "Account Details" and your Steam ID shows below your username
	-Input your Steam ID(this is for finding the save path and changing the save before the game loads them)

Now when launching the game you will automaticlly download the uploaded save file from Supabase and when quitting the game you will automaticlly upload the current save file to the Supabase storage, all your fiends need to do is have the mod installed and the same config as you(Besides the steam ID) and you will be sharing the save file with them!
