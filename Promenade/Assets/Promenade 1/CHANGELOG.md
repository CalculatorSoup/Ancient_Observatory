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
