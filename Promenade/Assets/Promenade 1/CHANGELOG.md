# 1.1.0
* Interactable changes:
  * Reduced interactable credits (580 -> 570) to match Helminth Hatchery
  * Turrets can no longer appear
  * Reduced selection weight for drone triple shops
  * Interactables should no longer be able to spawn on the log bridges
* Layout/Gameplay changes:
  * Added huge vases to the center of both large gazebos. One of the vases replaces the plus-shaped blockade that was in the middle of the gazebo closest to the large red building
  * Removed an awkwardly-placed ramp in front of the small red building and replaced it with a boulder
  * Added a few map node links that allow enemies to walk off of ledges
  * Removed ground nodes surrounding the artifact teleporter
    * These were placed intentionally, but I accidentally let interactables spawn on them and other Stage 5 maps don't have nodes around their artifact teleporters anyway
  * Fixed a few ground nodes that were floating or didn't have their gate set properly
* Lighting changes:
  * Added a "light cookie" (cloud shadows) and adjusted sun brightness to compensate. The map is probably a bit darker than it used to be
  * Simulacrum: Reduced shadow intensity (1 -> 0.3)
* Second art pass:
  * Updated the terrain material: dirt is redder, texture scale and blending was changed a bit. Cliffs are less pink and slightly darker. Color palette should now match both types of building in the map more closely
  * Slightly tweaked the terrain mesh to fix a couple spots where the dirt paths looked unnaturally jagged
  * Replaced the props in the cave to better fit with the rest of the map: Round pillars replaced with the red stone pillars, gemstones replaced by the glowing layered blockades found elsewhere
  * Added more stalactites and stalagmites to the cave and underhang
  * Added and moved hanging moss clusters around the map
  * Replaced the spiky rock model with one that's lower poly and hopefully fits in more with the rest of the map's terrain

# 1.0.1
* Added some platforms above the artifact teleporter ledge to drop down without taking lethal fall damage
* The artifact teleporter is now added using LocationsOfPrecipitation's script
* Very slightly edited the terrain mesh's vertex colors

# 1.0.0
* Added an artifact teleporter, hidden under a building (thanks to Viliger for providing the scripts used to add this!)

# 0.2.2
* Added Simplified Chinese translation (thanks to JunJun5406 on github for submitting this!)
* Fixed the materials for gazebos, obelisks, and other models using the 'Two Tone' ramp choice setting instead of 'Smoothed Two Tone'. Lighting on those objects should now look a bit less "harsh" and better overall
* Orrery "sun" texture is now slightly animated
* Fixed at least one of the draped cloths having collision even though it wasn't supposed to
* Updated code for adding monsters from EnemiesReturns to not use deprecated config options

# 0.2.1
* Removed Mini Mushrums and Elder Lemurians from the regular variant's spawn pool. Moved Stone Golems from basic monsters to minibosses
  * As mentioned in the notes for 0.2.0, the map had too many unique enemies, so I cut a couple. The base game monster selection now has 3 basic monsters, 3 minibosses, and 4 champions, matching vanilla Stage 5 maps
* Changed spawn distance for Children (Far -> Standard)
* **Simulacrum:** Removed Lesser Wisps. Mini Mushrums can still appear

# 0.2.0
* Did a quick art pass
  * Edited and cleaned up terrain topology. Cliffs should look a bit more eroded and dirt paths are slightly indented
  * The cave now has a mostly-dirt floor (was previously just grass)
  * Added tables, banners and other clutter in each building
  * Replaced the purple floating rocks in the background with other buildings orbiting the map
  * Added an aurora in the opposite direction from the moon
  * Slightly darkened the sun
  * Fixed some parts of the small pink ruins being noticeably off-center
* Added a new cliff face jutting out between the small pink ruins and the partially-crumbled gazebo
* Removed the 3 giant obelisks under the orrery
* Lesser Wisps can now spawn in both the regular and Simulacrum variants
  * I think I'm already bordering on having too many enemies in the stage (there's more than most vanilla maps, at least) but the map was missing any low credit cost basic enemies, so I felt like it was necessary to add these. Also the Greater Wisps were getting lonely I think
* **Simulacrum:** Halcyonites now only appear after 2 stage completions

# 0.1.0
* Initial Release
