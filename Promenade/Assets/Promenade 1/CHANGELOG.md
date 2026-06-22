# 2.0.0
This update is a complete overhaul of Ancient Observatory, almost from the ground up. After making four whole other maps following this one, I felt like I'd learned enough that I wanted to go back and polish this one. It's very different now in a lot of ways, but I think it's an improvement overall.

The map's visuals were redone (now set at night with more unique foliage and props) and the map's layout was changed beyond recognition in many areas to hopefully make it feel a little more "cohesive," or at least more fun to navigate. A config option was added to bring the map's lighting closer to pre-2.0 if you prefer the old look.

---

Also, this update includes a new music track, created by Jared Damron (@FakeBees on Discord) for the stage! Thanks to him for contributing it! You can also check out his other music on [YouTube](https://www.youtube.com/@jareddamron5977) if you enjoyed

---

- **Layout changes:**
  - The holes near the center of the map were filled in and the cave below the orrery (big floating thing with the big orb) was removed entirely. The floating gazebos were removed
  - Stretched out the hill that previously had a hole that fell into the cave. It's now directly connected to the orrery
  - The weird double-layer rock formation on the opposite side as the red buildings was removed and replaced with a new red building
  - Shrunk the gazebos, added a couple more, and added "layer cake" foundations with little ponds on the lower layers.
  - The large red building's second floor's giant gaping hole was filled up and the ramp to the second floor was moved closer to the walls. Launch pads to the second floor were added. The launch pad to reach the smaller building was replaced by a bridge
  - The small red building's second floor was removed, but a side balcony was added
  - Extra hills and cliffs were added near the small red building and the flat, empty area between the large red building and the orrery
  - Added several launch pads throughout the map
  - Removed most of the previous random variations since there was no spot left for them, really (but also they were mostly just weirdly-shaped floating rocks anyway...)

- **Visual changes:**
  - The map is now set at night and the sky is more dense with stars
    - Also added a config option to revert the lighting changes. You can also make it randomly swap between the old and new visuals if you'd like
  - The Conduit Canyon trees were replaced with new trees unique to this map. There is also a variety of other foliage now (glowing vines, bushes, stumps and fallen logs)
  - The lamps around the map were replaced by giant glowing bulb plants
  - Various pieces of furniture were added to make the buildings feel a little more "lived in" 
  - The orbiting structures in the skybox were removed. Instead, there are tall crags with structures and foliage placed on them
  - The almost-unnoticeable green dust motes are now much more visible. The wind gusts are now more subtle

- **Monster pool changes:**
  - Replaced Alloy Vultures with Imps
  - Replaced Children with Alpha Constructs
  - Replaced Grandparents with Solus Control Units
  - Removed Solus Amalgamators
  - Added Void Devastators (after looping)
  - Simulacrum variant now has largely the same monster pool, but with minor edits: Mini Mushrums appear, but Greater Wisps and Halcyonites do not
  - Added [Ancient Wisps](https://thunderstore.io/package/Moffein/Ancient_Wisp/)
    - Also added a config setting to toggle Ancient Wisps

- **Other changes:**
  - Added new stage music - *[Orb Knowledge](https://www.youtube.com/watch?v=8FRuaXpz70U)*
    - Boss music is still *Antarctic Oscillation*
    - Also added a dependency on R2API Sound
  - Swapped broken Junk Drones with Jailer Drones
  - Added a couple new Newt Altar spots and relocated one that was previously behind a pillar in an overhang (it is now behind a tree.)
  - The vertical banners on the central orrery now have collision

# 1.1.1
* Fixed Lemurian Eggs not spawning with Artifact of Devotion enabled

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