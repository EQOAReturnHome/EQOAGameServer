local ch = tostring(choice)
function event_say()
   local diagOptions = {}
   local questText = ""
   local npcDialogue = ""
--Human
if(race == "Human")then
    --Freeport/Eastern
    if(humanType == "Eastern")then
     if(class == "Magician")then
        multiDialogue = { "Welcome to Freeport young magician. This is the city of trade and the eastern home of the humans.",
    "As a new magician you been invited to join the Academy of Arcane Science. They are the Magician's guild here in Freeport.",
    "Before we get too far, do you need instructions on how to use basic controls?" }
     npcDialogue = "You are now ready to move on to your first quest. Speak to Malsis, he is the guildmaster standing before you."
      elseif(class == "Necromancer")then
     multiDialogue = { "Welcome to Freeport young necromancer. This is the city of trade and the eastern home of the humans.",
       "As a new necromancer, you have been invited to join the House Slaerin. They are the necromancer's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Corious Slaerin, he is the guildmaster standing before you."
      elseif(class == "Alchemist")then
     multiDialogue = { "Welcome to Freeport young alchemist. This is the city of trade and the eastern home of the humans.",
       "As a new alchemist, you have been invited to join the Academy of Arcane Science. They are the alchemist's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Dilina McNerian, she is the guildmaster standing before you."
      elseif(class == "Bard")then
     multiDialogue = { "Welcome to Freeport young bard. This is the city of trade and the eastern home of the humans.",
       "As a new bard, you have been invited to join The Silken Gauntlet. They are the bard's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to William Corufost, he is the guildmaster standing before you."
      elseif(class == "Cleric")then
     multiDialogue = { "Welcome to Freeport young cleric. This is the city of trade and the eastern home of the humans.",
       "As a new cleric, you have been invited to join The Spiteful Shield. They are the cleric's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Denouncer Alshea, she is the guildmaster standing before you."
      elseif(class == "Enchanter")then
     multiDialogue = { "Welcome to Freeport young enchanter. This is the city of trade and the eastern home of the humans.",
       "As a new enchanter, you have been invited to join the Academy of Arcane Science. They are the enchanter's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Azlynn, she is the guildmaster standing before you."
      elseif(class == "Rogue")then
     multiDialogue = { "Welcome to Freeport young rogue. This is the city of trade and the eastern home of the humans.",
       "As a new rogue, you have been invited to join The Spiteful Shield. They are the rogue's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Necorik the Ghost, he is the guildmaster standing before you."
      elseif(class == "ShadowKnight")then
     multiDialogue = { "Welcome to Freeport young shadowknight. This is the city of trade and the eastern home of the humans.",
       "As a new shadowknight, you have been invited to join The Spiteful Shield. They are the shadowknight's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Malethai Crimsonhand, he is the guildmaster standing before you."
      elseif(class == "Warrior")then
     multiDialogue = { "Welcome to Freeport young warrior. This is the city of trade and the eastern home of the humans.",
       "As a new warrior, you have been invited to join The Freeport Militia. They are the warrior's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Commander Nothard, he is the guildmaster standing before you."
       elseif(class == "Wizard")then
     multiDialogue = { "Welcome to Freeport young wizard. This is the city of trade and the eastern home of the humans.",
       "As a new wizard, you have been invited to join the Academy of Arcane Science. They are the wizard's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Sivrendesh, he is the guildmaster standing before you."
       end
    --Qeynos/Western
    elseif(humanType == "Western")
      if(class == "Alchemist")then
        multiDialogue = { "Welcome to Qeynos playerName. This is the western home of the humans and the newly crowned king, Antonius Bayle II.",
"As a new alchemist, you have been invited to join the Anagogical Order. They are the alchemist's guild here in Qeynos.",
"Before we get too far, do you need instructions on how to use the basic controls?",
"Then you are ready to move on to your first quest. Speak to Jergish Anaebarum, he is the guildmaster standing before you."
 }
     npcDialogue = "You are now ready to move on to your first quest. Speak to Malsis, he is the guildmaster standing before you."
      elseif(class == "Bard")then
     multiDialogue = { "Welcome to Freeport young necromancer. This is the city of trade and the eastern home of the humans.",
       "As a new necromancer, you have been invited to join the House Slaerin. They are the necromancer's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Corious Slaerin, he is the guildmaster standing before you."
      elseif(class == "Cleric")then
     multiDialogue = { "Welcome to Freeport young alchemist. This is the city of trade and the eastern home of the humans.",
       "As a new alchemist, you have been invited to join the Academy of Arcane Science. They are the alchemist's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Dilina McNerian, she is the guildmaster standing before you."
      elseif(class == "Druid")then
     multiDialogue = { "Welcome to Freeport young bard. This is the city of trade and the eastern home of the humans.",
       "As a new bard, you have been invited to join The Silken Gauntlet. They are the bard's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to William Corufost, he is the guildmaster standing before you."
      elseif(class == "Cleric")then
     multiDialogue = { "Welcome to Freeport young cleric. This is the city of trade and the eastern home of the humans.",
       "As a new cleric, you have been invited to join The Spiteful Shield. They are the cleric's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Denouncer Alshea, she is the guildmaster standing before you."
      elseif(class == "Druid")then
     multiDialogue = { "Welcome to Freeport young enchanter. This is the city of trade and the eastern home of the humans.",
       "As a new enchanter, you have been invited to join the Academy of Arcane Science. They are the enchanter's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Azlynn, she is the guildmaster standing before you."
      elseif(class == "Enchanter")then
     multiDialogue = { "Welcome to Freeport young rogue. This is the city of trade and the eastern home of the humans.",
       "As a new rogue, you have been invited to join The Spiteful Shield. They are the rogue's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Necorik the Ghost, he is the guildmaster standing before you."
      elseif(class == "Magician")then
     multiDialogue = { "Welcome to Freeport young shadowknight. This is the city of trade and the eastern home of the humans.",
       "As a new shadowknight, you have been invited to join The Spiteful Shield. They are the shadowknight's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Malethai Crimsonhand, he is the guildmaster standing before you."
      elseif(class == "Monk")then
     multiDialogue = { "Welcome to Freeport young warrior. This is the city of trade and the eastern home of the humans.",
       "As a new warrior, you have been invited to join The Freeport Militia. They are the warrior's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Commander Nothard, he is the guildmaster standing before you."
       elseif(class == "Paladin")then
     multiDialogue = { "Welcome to Freeport young wizard. This is the city of trade and the eastern home of the humans.",
       "As a new wizard, you have been invited to join the Academy of Arcane Science. They are the wizard's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Sivrendesh, he is the guildmaster standing before you."
     elseif(class == "Ranger")then
     multiDialogue = { "Welcome to Freeport young rogue. This is the city of trade and the eastern home of the humans.",
       "As a new rogue, you have been invited to join The Spiteful Shield. They are the rogue's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Necorik the Ghost, he is the guildmaster standing before you."
      elseif(class == "Rogue")then
     multiDialogue = { "Welcome to Freeport young shadowknight. This is the city of trade and the eastern home of the humans.",
       "As a new shadowknight, you have been invited to join The Spiteful Shield. They are the shadowknight's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Malethai Crimsonhand, he is the guildmaster standing before you."
      elseif(class == "Warrior")then
     multiDialogue = { "Welcome to Freeport young warrior. This is the city of trade and the eastern home of the humans.",
       "As a new warrior, you have been invited to join The Freeport Militia. They are the warrior's guild here in Freeport.",
       "Before we get too far, do you need instructions on how to use the basic controls?" }
     npcDialogue = "Then you are ready to move on to your first quest. Speak to Commander Nothard, he is the guildmaster standing before you."
       end
     SetPlayerFlags(mySession, "NewPlayerIntro", "1")
    end
end
    SendMultiDialogue(mySession, multiDialogue)
    SendDialogue(mySession, npcDialogue, diagOptions)
end
