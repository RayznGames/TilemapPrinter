# TilemapPrinter
A Unity 2D tilemap printer

These are 2 short scripts that can help you place prefabbed tilemaps, into one or multiple tilemaps of your game at Runtime. Or in the editor if you create a Custom Editor for it

# How it works
The tilemap printer scans a tilemap you provide. It ensures the coordinate position at the bottom-most left side of the tilemap to start at 0,0, ensuring the printable's positioning for later printing.
Once the scan has finished and the tilemap is stored we, choose a position to start printing the scanned tilemap over the world tilemap.
This all happens in the function PrintTilemap() and PrintMultiLayerTilemap()

# Tilemap Printer
Tilemap printer prints only the provided printable tilemap into the provided world tilemap. This simple this easy.

# Multi Tilemap Printer
Multi Tilemap Printer prints an array of tilemaps over an array of provided world tilemaps. It comes useful when having tilemaps over tilemaps for background and foreground use. 
It is important to set up the Array indexing order correctly in both arrays:

**tilemapsToPrint** [0] = PrintableBackground 

**tilemapsToPrint** [1] = PrintableGround



**worldsToWrite** [0] = WorldBackground

**worldsToWrite** [1] = WorldGround

This is all it does, since my needs are running this for a step in a terrain generator, I have not invested much time to make this an editor-friendly tool, pretty much a script-friendly one, However you can make a Custom Editor to place the tilemaps manually in the UnityEditor, I would be more than happy to make it myself one day if I have more free time. 
