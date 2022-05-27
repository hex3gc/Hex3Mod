# Info

Adds 1 artifact and 16 new items to the game:
* 5 Common
* 2 Uncommon
* 3 Legendary
* 6 Void

![Items1](https://i.imgur.com/zHOAmGH.png)

Each item has a unique effect and configurable values. Special thanks to the RoR2 Modding Discord for teaching me how to do this.

# To do:

* Create more items
* Assess balance and bugginess
* Ensure mod compatibility

# Bugs

* Please give feedback/bug reports on the RoR2 Modding discord, or by messaging directly: hex3#7952

# Changelog

### 0.4.1
* Updated for new patch
* Put all the files on GitHub (DISCLAIMER: All of the code in this project is written by someone who learned C# very recently. A rewrite is in order, but not today...)

### 0.4.0
* Added item "Drop Of Necrosis" (Void common)
* Added item "Spattered Collection" (Void uncommon)

### 0.3.4
* Added character item displays for all items (No modded character compatibility yet)
* Empathy no longer triggers from void fog damage
* Apathy no longer triggers from void fog damage
* Updated some item icons

### 0.3.3
* Added item "Elder Mutagen" (Legendary)
* Mint Condition should no longer prevent Hunter's Harpoon from activating

### 0.3.2
* Discovery's stack limit should now scale correctly
* Discovery should no longer add any stacks above its limit
* Added speed cap for Notice Of Absence (default 500%) to prevent uncontrollably high speeds
* Fixed some clerical errors

### 0.3.1
* Added item "400 Tickets" (Common)
* Added visual and sound effects for Discovery
* Added sound effects for Scattered Reflection
* New buff icons for Discovery, The Hermit and Apathy

### 0.3.0
* VFX Overhaul part 2: New models, icons and shaders for all items
* Added 'Alternate Mode' and 'No Replication' config options for Corrupting Parasite.
* Scattered Reflection should no longer attack teammates or proc itself

### 0.2.5
* Added item "The Hermit" (Void legendary)
* Adjusted the sizes of some item models
* Fixed an NRE caused by the Newt
* Made Apathy's barrier gain cooldown more consistent

### 0.2.4
* Added artifact "Artifact Of Corruption"

### 0.2.3 - Void Update, pt 1
* Added item "Corrupting Parasite". Thanks to kking and conq for giving me the idea for the item and allowing me to use it (Void common)
* Added item "Notice of Absence" (Void common)
* Added item "Discovery" (Void uncommon)
* Apathy damage reduction reverted to 20% (+10%)
* Apathy barrier on hit 5% (+2%) -> 3% (+2%)

### 0.2.2
* Added item "Mint Condition" (Legendary)

### 0.2.1
You may need to change your config files for some of these changes to take effect.
* Added AI blacklist to Apathy to prevent invincible enemies
* Apathy damage reduction 20% (+10%) -> 30% (+15%).
* Apathy 10% barrier requirement for damage reduction -> 5%
* Apathy description cleared up
* Scattered Reflection proc coefficient 0 -> 1
* Hopoo Egg jump power modifier 10% -> 15%
* Hopoo Egg air control modifier 5% -> 10%

### 0.2.0
* Added item "Apathy" (Legendary)

### 0.1.6
* VFX Overhaul: New models, icons and shaders for all items

### 0.1.5
* Added item "ATG Prototype" (Common)

### 0.1.4
* (Hopefully) fixed another NRE caused by Scattered Reflection interacting with Blood Shrines and Void Cradles incorrectly
* Removed the non-functional Common item accidentally included with 0.1.3

### 0.1.2
* Fixed an NRE caused by Scattered Reflection

### 0.1.1
* Added item "Empathy" (Uncommon)

### 0.1.0
* Initial release