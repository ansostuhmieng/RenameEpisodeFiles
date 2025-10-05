# Ansos' Episode File Renamer

This tools renames batches of files for a specific TV show, based on data from [TheTVDB.com](https://www.thetvdb.com/), including the episode title. 

It is specifically designed for users of tools like Plex or Embi who are making backups of TV shows from DVDs or Blu-Rays.

## Default Mode

![Screenshot of Rename Episodes app](renep-default.png)

### Options

- Folder Path - folder where the files are now. Use the '?' button to select from a dialog.
- Season Number - number of the season you are renaming. Numbers only.
- Starting Episode Number - first episode found in the folder, will auto-increment to the 'next' episode number after the folder is completed.
- Episode Data - a text file of data copied from theTVDB.com for the season containing all the episodes one per line
  > If you copy the table into a text file from theTVDB.com season page, you can use the 'Clean Episode Data' button to should clean it up for use
- Show Name - Where the show name goes that will be used in the filename
- Copy Files To [Optional, Experimental] - Will try and copy the files to a destination location if filled

### Caveats

- Paths should not be wrapped in quotes
- It assumes the Folder Path **ONLY** has episodes in it, so be aware of that
  - It orders the files by creation timestamp and does it's thing
- No real safety nets
  - Renames cannot be automatically undone
  - Overwrites any existing filenames
  
### Renaming

- Click the 'Rename Files' button to fire it off
  - Files will be renamed in the format `<Show Name>.S<Season Number>E<Episode Number>.<Episode Title [from Episode Data]>.<Original File Extension>`
  - for example: `Star Trek - Deep Space Nine.S03E11.Past Tense, Part I.mkv`
