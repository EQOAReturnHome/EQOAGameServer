--Default Spells for classes
local spell = 0
local items = require('Scripts/items')
local spells = require('Scripts/spells')


function CharacterDefaults()
   AddDefaultSpell(serverID, spells.RETURN_HOME, 0) --Return Home 
   --Warrior
   if(class == "Warrior") then
      AddDefaultSpell(serverID, spells.QUICK_STRIKE, 1) --Quickstrike
      AddDefaultGear(serverID, items.APPRENTICE_SWORD, 1)
      AddRations()
      --Ranger
   elseif(class == "Ranger")then
      AddDefaultSpell(serverID, spells.THISTLECOAT, 1) --Thistlecoat
      AddDefaultGear(serverID, items.APPRENTICE_SWORD, 1)
      AddRations()
      --Paladin
   elseif(class == "Paladin")then
      AddDefaultSpell(serverID, spells.DIVINE_STRIKE, 1) --Divine Strike
      AddDefaultGear(serverID, items.APPRENTICE_SWORD, 1)
      AddRations()
      --Shadowknight
   elseif(class == "ShadowKnight")then
      AddDefaultSpell(serverID, spells.CHILLING_AURA, 1) --Chilling Aura
      AddDefaultGear(serverID, items.APPRENTICE_SWORD, 1)
      AddRations()
      --Monk
   elseif(class == "Monk")then
      AddDefaultSpell(serverID, spells.MEND, 1) --Mend
      AddRations()
      --Bard
   elseif(class == "Bard")then
      AddDefaultSpell(serverID, spells.NIMBLE_STRIKE, 1) --Nimble Strike
      AddDefaultGear(serverID, items.APPRENTICE_DAGGER, 1)
      AddRations()
      --Rogue
   elseif(class == "Rogue")then
      AddDefaultSpell(serverID, spells.HIDE, 1) --Hide
      AddDefaultGear(serverID, items.APPRENTICE_DAGGER, 1)
      AddRations()
      --Druid
   elseif(class == "Druid")then
      AddDefaultSpell(serverID, spells.MINOR_HEALING, 1) --Minor Healing
      AddDefaultGear(serverID, items.APPRENTICE_CLUB, 1)
      AddRations()
      --Shaman
   elseif(class == "Shaman")then
      AddDefaultSpell(serverID, spells.MINOR_HEALING, 1) --Minor Healing
      AddDefaultGear(serverID, items.APPRENTICE_CLUB, 1)
      AddRations()
      --Cleric
   elseif(class == "Cleric")then
      AddDefaultSpell(serverID, spells.MINOR_HEALING, 1) --Minor Healing
      AddDefaultGear(serverID, items.APPRENTICE_CLUB, 1)
      AddRations()
      --Magician
   elseif(class == "Magician")then
      AddDefaultSpell(serverID, spells.BURST_OF_FROST, 1) --Burst of Frost
      AddDefaultGear(serverID, items.APPRENTICE_STAFF, 1)
      AddRations()
      --Necromancer
   elseif(class == "Necromancer")then
      AddDefaultSpell(serverID, spells.DISEASE_CLOUD, 1) --Disease Cloud
      AddDefaultGear(serverID, items.APPRENTICE_STAFF, 1)
      AddRations()
      --Enchanter
   elseif(class == "Enchanter")then
      AddDefaultSpell(serverID, spells.LULL, 1) --Lull
      AddDefaultGear(serverID, items.APPRENTICE_STAFF, 1)
      AddRations()
      --Wizard
   elseif(class == "Wizard")then
      AddDefaultSpell(serverID, spells.STATIC_BOLT, 1) --Static Bolt
      AddDefaultGear(serverID, items.APPRENTICE_STAFF, 1)
      AddRations()
      --Alchemist
   elseif(class == "Alchemist")then
      AddDefaultSpell(serverID, spells.UNSTABLE_MIXTURE, 1) --Unstable Mixture
      AddDefaultGear(serverID, items.APPRENTICE_STAFF, 1)
      AddRations()
  end
end


function AddRations()
      --Human
   if(race == "Human")then
      if(humanType == "Eastern")then
         AddDefaultGear(serverID, items.DESERT_RATIONS, 20) --Desert Rations
      else
         AddDefaultGear(serverID, items.FIELD_RATIONS, 20) --Field Rations
      end
      --Elf
   elseif(race == "Elf")then
      AddDefaultGear(serverID, items.TRAIL_RATIONS, 20) --Trail Rations
      --Dark Elf
   elseif(race == "Dark Elf")then
      AddDefaultGear(serverID, items.TRAVEL_CUISINE, 20) --Travel Cuisine
      --Gnome
   elseif(race == "Gnome")then
      AddDefaultGear(serverID, items.GNOMISH_RATIONS, 20) --Gnomish Rations
      --Dwarf
   elseif(race == "Dwarf")then
      AddDefaultGear(serverID, items.MINE_RATIONS, 20) --Mine Rations
      --Troll
   elseif(race == "Troll")then
      AddDefaultGear(serverID, items.TOAD_SNACKS, 20) --Toad Snacks
      --Barbarian
   elseif(race == "Barbarian")then
      AddDefaultGear(serverID, items.TUNDRA_RATIONS, 20) --Tundra Rations
      --Halfling
   elseif(race == "Halfling")then
      AddDefaultGear(serverID, items.THICKET_RATIONS, 20) --Thicket Rations
      --Erudite
   elseif(race == "Erudite")then
      AddDefaultGear(serverID, items.PLAINS_RATIONS, 20) --Plains Rations
      --Ogre
   elseif(race == "Ogre")then
      AddDefaultGear(serverID, items.JUNGLE_RATIONS, 20) --Jungle Rations
   end
end


