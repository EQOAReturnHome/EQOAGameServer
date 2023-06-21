local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Magician(10) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "100110") == "1") then
if (choice:find("don\'t")) then
multiDialogue = { "Guard Sareken: Oh, that's too bad. We really could have used the help."
 } 
elseif (choice:find("am")) then
multiDialogue = { "Guard Sareken: Very good. Listen, things have been rough for the guards here at Freeport as of late. I've been having trouble keeping my men sufficiently supplied. I could use a little help.",
    "Guard Sareken: Would you be willing to donate to the protection of Freeport? I need 147 Tunar for our current shortage.",
    "Guard Sareken: I'll also need three tough pike scales for our equipment. If you head north, you can find large pike in the river.",
    "Guard Sareken: Return to me with 147 tunar and three tough pike scales and you will be rewarded.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100110, quests[100110][1].log)
else
    npcDialogue = "Ah yes, are you the assistant that Kellina has sent?"
    diagOptions = { "I am.", "I don't know what you mean..." }
end
elseif (GetPlayerFlags(mySession, "100110") == "2") then
if (mySession.MyCharacter.Inventory.Tunar >= 147
and CheckQuestItem(mySession, items.TOUGH_PIKE_SCALE, 3))
 then
if (choice:find("Whoops")) then
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with 147 tunar and three tough pike scales and you will be rewarded."
elseif (choice:find("right")) then
multiDialogue = { "Guard Sareken: Impressive. It seems as though Kellina has been doing a better job with the Academy of Arcane Science as of late, if your work here is anything to show for it.",
    "Guard Sareken: This will no doubt bolster our supplies. Thank you playerName.",
    "Guard Sareken: I'm afraid I'll need your help again, however. Scouts have reported that a dangerous orc has been sighted around that old house north of Freeport.",
    "Guard Sareken: Go investigate it at once. If you find the orc, destroy it and bring me its club as proof.",
"You have given away 147 Tunar.",
"You have given away a tough pike scale.",
"You have given away a tough pike scale.",
"You have given away a tough pike scale.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100110, quests[100110][2].log)
RemoveTunar(mySession, 147 )
TurnInItem(mySession, items.TOUGH_PIKE_SCALE, 3)
else
    npcDialogue = "Have you returned with the supplies already?"
    diagOptions = { "Yes. I have it all right here.", "Whoops, not yet." }
end
else
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with 147 tunar and three tough pike scales and you will be rewarded."
end
elseif (GetPlayerFlags(mySession, "100110") == "3") then
if (CheckQuestItem(mySession, items.ORC_RANSACKERS_CLUB, 1))
 then
if (choice:find("don\'t")) then
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with the orc ransacker club, which can be found near the old house north of Freeport."
elseif (choice:find("wasn\'t")) then
multiDialogue = { "Guard Sareken: Yes, this does appear to be the club of such an orc. It is quite alarming that such a foul creature has wandered this close to the city. I must investigate further.",
    "Guard Sareken: playerName, you have proven yourself worthy as a magician of The Academy of Arcane Science. Kellina will hear of my highest marks for your performance.",
    "Guard Sareken: As your reward, I offer you this Sturdy Staff. May you use it well as you study the ways of battle.",
    "Guard Sareken: I am certain that with your talent, Kellina will call upon you again.",
"You have given away an orc ransacker's club.",
"You have received a Sturdy Staff.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 100110, quests[100110][3].xp, 100113 )
TurnInItem(mySession, items.ORC_RANSACKERS_CLUB, 1)
GrantItem(mySession, items.STURDY_STAFF, 1)
else
    npcDialogue = "Do you have the club?"
    diagOptions = { "It wasn't easy, but yes.", "I don't think so..." }
end
else
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with the orc ransacker club, which can be found near the old house north of Freeport."
end
  else
        npcDialogue =
"Guard Sareken: No one realizes how difficult this job actually is, playerName. It's blazing hot, criminals are everywhere, and we don't even have all the supplies to do our work. Some days, I think about changing to a simpler life."
    end
------
--Bard(5) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "50110") == "1") then
if (choice:find("don\'t")) then
multiDialogue = { "Guard Sareken: Oh, I had requested help from Solenia Freyar, I thought maybe she sent you. If you speak to her, please mention that I could still really use the help."
 } 
elseif (choice:find("am")) then
multiDialogue = { "Guard Sareken: Very good. Listen, things have been rough for the guards here at Freeport as of late. I've been having trouble keeping my men sufficiently supplied. I could use a little help.",
    "Guard Sareken: Would you be willing to donate to the protection of Freeport? I need 147 Tunar for our current shortage.",
    "Guard Sareken: I'll also need three tough pike scales for our equipment. If you head north, you can find large pike in the river.",
    "Guard Sareken: Return to me with 147 tunar and three tough pike scales and you will be rewarded.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50110, quests[50110][1].log)
else
    npcDialogue = "Ah yes, are you the assistant that Solenia Freyar has sent?"
    diagOptions = { "I am.", "I don't know what you mean..." }
end
elseif (GetPlayerFlags(mySession, "50110") == "2") then
if (mySession.MyCharacter.Inventory.Tunar >= 147
and CheckQuestItem(mySession, items.TOUGH_PIKE_SCALE, 3))
 then
if (choice:find("Whoops")) then
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider gathering what I have requested for our cause. Return to me with 147 tunar and three tough pike scales and you will be rewarded."
elseif (choice:find("right")) then
multiDialogue = { "Guard Sareken: Impressive. It seems as though Solenia has been doing a better job with the Silken Gauntlet as of late, if your work here is anything to show for it.",
    "Guard Sareken: This will no doubt bolster our supplies. Thank you playerName.",
    "Guard Sareken: I'm afraid I'll need your help again, however. Scouts have reported that a dangerous orc has been sighted around that old house north of Freeport.",
    "Guard Sareken: Go investigate it at once. If you find the orc, destroy it and bring me its club as proof.",
"You have given away 147 Tunar.",
"You have given away a tough pike scale.",
"You have given away a tough pike scale.",
"You have given away a tough pike scale.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50110, quests[50110][2].log)
RemoveTunar(mySession, 147 )
TurnInItem(mySession, items.TOUGH_PIKE_SCALE, 3)
else
    npcDialogue = "Have you returned with the supplies already?"
    diagOptions = { "Yes. I have it all right here.", "Whoops, not yet." }
end
else
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider gathering what I have requested for our cause. Return to me with 147 tunar and three tough pike scales and you will be rewarded."
end
elseif (GetPlayerFlags(mySession, "50110") == "3") then
if (CheckQuestItem(mySession, items.ORC_RANSACKERS_BOOTS, 1))
 then
if (choice:find("don\'t")) then
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider gathering what I have requested for our cause. Return to me with the orc ransacker's boots, which can be found near the old house north of Freeport."
elseif (choice:find("wasn\'t")) then
multiDialogue = { "Guard Sareken: Yes, this does appear to be the boots of such an orc. It is quite alarming that such a foul creature has wandered this close to the city. I must investigate further.",
    "Guard Sareken: playerName, you have proven yourself worthy as a bard of The Silken Gauntlet. Solenia will hear of my highest marks for your performance.",
    "Guard Sareken: As your reward, I offer you this Sturdy Short Sword. May you use it well as you study the ways of battle.",
    "Guard Sareken: I am certain that with your talent, Solenia will call upon you again.",
"You have given away an orc ransacker's boots.",
"You have received a Sturdy Short Sword.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 50110, quests[50110][3].xp, 50113 )
TurnInItem(mySession, items.ORC_RANSACKERS_BOOTS, 1)
GrantItem(mySession, items.STURDY_SHORT_SWORD, 1)
else
    npcDialogue = "Do you have the orc ransacker's boots?"
    diagOptions = { "It wasn't easy, but yes.", "I don't think so..." }
end
else
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider gathering what I have requested for our cause. Return to me with the orc ransacker's boots, which can be found near the old house north of Freeport."
end
  else
        npcDialogue =
"Guard Sareken: No one realizes how difficult this job actually is, playerName. It's blazing hot, criminals are everywhere, and we don't even have all the supplies to do our work. Some days, I think about changing to a simpler life."
    end
------
--Cleric(9) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "90110") == "1") then
if (choice:find("don\'t")) then
multiDialogue = { "Guard Sareken: Oh, that's too bad. We really could have used the help."
 } 
elseif (choice:find("am")) then
multiDialogue = { "Guard Sareken: Very good. Listen, things have been rough for the guards here at Freeport as of late. I've been having trouble keeping my men sufficiently supplied. I could use a little help.",
    "Guard Sareken: Would you be willing to donate to the protection of Freeport? I need 147 Tunar for our current shortage.",
    "Guard Sareken: I'll also need three tough pike scales for our equipment. If you head north, you can find large pike in the river.",
    "Guard Sareken: Return to me with 147 tunar and three tough pike scales and you will be rewarded.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90110, quests[90110][1].log)
else
    npcDialogue = "Ah yes, are you the assistant that Sister Falhelm has sent?"
    diagOptions = { "I am.", "I don't know what you mean..." }
end
elseif (GetPlayerFlags(mySession, "90110") == "2") then
if (mySession.MyCharacter.Inventory.Tunar >= 147
and CheckQuestItem(mySession, items.TOUGH_PIKE_SCALE, 3))
 then
if (choice:find("Whoops")) then
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with 147 tunar and three tough pike scales and you will be rewarded."
elseif (choice:find("right")) then
multiDialogue = { "Guard Sareken: Impressive. It seems as though Sister Falhelm has been doing a better job with the Shining Shield Mercenaries as of late, if your work here is anything to show for it.",
    "Guard Sareken: This will no doubt bolster our supplies. Thank you playerName.",
    "Guard Sareken: I'm afraid I'll need your help again, however. Scouts have reported that a dangerous orc has been sighted around that old house north of Freeport.",
    "Guard Sareken: Go investigate it at once. If you find the orc, destroy it and bring me its pouch as proof.",
"You have given away 147 Tunar.",
"You have given away a tough pike scale.",
"You have given away a tough pike scale.",
"You have given away a tough pike scale.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90110, quests[90110][2].log)
RemoveTunar(mySession, 147 )
TurnInItem(mySession, items.TOUGH_PIKE_SCALE, 3)
else
    npcDialogue = "Have you returned with the supplies already?"
    diagOptions = { "Yes. I have it all right here.", "Whoops, not yet." }
end
else
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with 147 tunar and three tough pike scales and you will be rewarded."
end
elseif (GetPlayerFlags(mySession, "90110") == "3") then
if (CheckQuestItem(mySession, items.ORC_RANSACKERS_POUCH, 1))
 then
if (choice:find("don\'t")) then
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with the orc ransacker's pouch, which can be found near the old house north of Freeport."
elseif (choice:find("wasn\'t")) then
multiDialogue = { "Guard Sareken: Yes, this does appear to be the pouch of such an orc. It is quite alarming that this dangerous creature has wandered this close to the city. I must investigate further.",
    "Guard Sareken: playerName, you have proven yourself worthy as a cleric of The Shining Shield Mercenaries. Sister will hear of my highest marks for your performance.",
    "Guard Sareken: As your reward, I offer you this Sturdy Mace. May you use it well as you study the ways of battle.",
    "Guard Sareken: I am certain that with your talent, Sister will call upon you again.",
"You have given away an orc ransacker's pouch.",
"You have received a Sturdy Mace.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 90110, quests[90110][3].xp, 90113 )
TurnInItem(mySession, items.ORC_RANSACKERS_POUCH, 1)
GrantItem(mySession, items.STURDY_MACE, 1)
else
    npcDialogue = "Do you have the pouch?"
    diagOptions = { "It wasn't easy, but yes.", "I don't think so..." }
end
else
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with the orc ransacker's pouch, which can be found near the old house north of Freeport."
end
  else
        npcDialogue =
"Guard Sareken: No one realizes how difficult this job actually is, playerName. It's blazing hot, criminals are everywhere, and we don't even have all the supplies to do our work. Some days, I think about changing to a simpler life."
    end
------
--Rogue(6) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "60110") == "1") then
if (choice:find("don\'t")) then
multiDialogue = { "Guard Sareken: Oh, that's too bad. We really could have used the help."
 } 
elseif (choice:find("am")) then
multiDialogue = { "Guard Sareken: Very good. Listen, things have been rough for the guards here at Freeport as of late. I've been having trouble keeping my men sufficiently supplied. I could use a little help.",
    "Guard Sareken: Would you be willing to donate to the protection of Freeport? I need 147 Tunar for our current shortage.",
    "Guard Sareken: I'll also need three tough pike scales for our equipment. If you head north, you can find large pike in the river.",
    "Guard Sareken: Return to me with 147 tunar and three tough pike scales and you will be rewarded.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60110, quests[60110][1].log)
else
    npcDialogue = "Ah yes, are you the assistant that Athera has sent?"
    diagOptions = { "I am.", "I don't know what you mean..." }
end
elseif (GetPlayerFlags(mySession, "60110") == "2") then
if (mySession.MyCharacter.Inventory.Tunar >= 147
and CheckQuestItem(mySession, items.TOUGH_PIKE_SCALE, 3))
 then
if (choice:find("Whoops")) then
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with 147 tunar and three tough pike scales and you will be rewarded."
elseif (choice:find("right")) then
multiDialogue = { "Guard Sareken: Impressive. It seems as though Athera has been doing a better job with the Shining Shield Mercenaries as of late, if your work here is anything to show for it.",
    "Guard Sareken: This will no doubt bolster our supplies. Thank you playerName.",
    "Guard Sareken: I'm afraid I'll need your help again, however. Scouts have reported that a dangerous orc has been sighted around that old house north of Freeport.",
    "Guard Sareken: Go investigate it at once. If you find the orc, destroy it and bring me its belt as proof.",
"You have given away 147 Tunar.",
"You have given away a tough pike scale.",
"You have given away a tough pike scale.",
"You have given away a tough pike scale.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60110, quests[60110][2].log)
RemoveTunar(mySession, 147 )
TurnInItem(mySession, items.TOUGH_PIKE_SCALE, 3)
else
    npcDialogue = "Have you returned with the supplies already?"
    diagOptions = { "Yes. I have it all right here.", "Whoops, not yet." }
end
else
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with 147 tunar and three tough pike scales and you will be rewarded."
end
elseif (GetPlayerFlags(mySession, "60110") == "3") then
if (CheckQuestItem(mySession, items.ORC_RANSACKERS_BELT, 1))
 then
if (choice:find("don\'t")) then
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with the orc ransacker's belt, which can be found near the old house north of Freeport."
elseif (choice:find("wasn\'t")) then
multiDialogue = { "Guard Sareken: Yes, this does appear to be the belt of such an orc. It is quite alarming that this dangerous creature has wandered this close to the city. I must investigate further.",
    "Guard Sareken: playerName, you have proven yourself worthy as a rogue of The Shining Shield Mercenaries. Athera will hear of my highest marks for your performance.",
    "Guard Sareken: As your reward, I offer you this Sturdy Dagger. May you use it well as you study the ways of battle.",
    "Guard Sareken: I am certain that with your talent, Athera will call upon you again.",
"You have given away an orc ransacker's belt.",
"You have received a Sturdy Dagger.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 60110, quests[60110][3].xp, 60113 )
TurnInItem(mySession, items.ORC_RANSACKERS_BELT, 1)
GrantItem(mySession, items.STURDY_DAGGER, 1)
else
    npcDialogue = "Do you have the belt?"
    diagOptions = { "It wasn't easy, but yes.", "I don't think so..." }
end
else
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with the orc ransacker's belt, which can be found near the old house north of Freeport."
end
  else
        npcDialogue =
"Guard Sareken: No one realizes how difficult this job actually is, playerName. It's blazing hot, criminals are everywhere, and we don't even have all the supplies to do our work. Some days, I think about changing to a simpler life."
    end
------
--Warrior(0) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "110") == "1") then
if (choice:find("don\'t")) then
multiDialogue = { "Guard Sareken: Oh, that's too bad. We really could have used the help."
 } 
elseif (choice:find("am")) then
multiDialogue = { "Guard Sareken: Very good. Listen, things have been rough for the guards here at Freeport as of late. I've been having trouble keeping my men sufficiently supplied. I could use a little help.",
    "Guard Sareken: Would you be willing to donate to the protection of Freeport? I need 147 Tunar for our current shortage.",
    "Guard Sareken: I'll also need three tough pike scales for our equipment. If you head north, you can find large pike in the river.",
    "Guard Sareken: Return to me with 147 tunar and three tough pike scales and you will be rewarded.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110, quests[110][1].log)
else
    npcDialogue = "Ah yes, are you the assistant that Captain Norgam has sent?"
    diagOptions = { "I am.", "I don't know what you mean..." }
end
elseif (GetPlayerFlags(mySession, "110") == "2") then
if (mySession.MyCharacter.Inventory.Tunar >= 147
and CheckQuestItem(mySession, items.TOUGH_PIKE_SCALE, 3))
 then
if (choice:find("Whoops")) then
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with 147 tunar and three tough pike scales and you will be rewarded."
elseif (choice:find("right")) then
multiDialogue = { "Guard Sareken: Impressive. It seems as though Captain Norgam has been doing a better job with the Freeport Militia as of late, if your work here is anything to show for it.",
    "Guard Sareken: This will no doubt bolster our supplies. Thank you playerName.",
    "Guard Sareken: I'm afraid I'll need your help again, however. Scouts have reported that a dangerous orc has been sighted around that old house north of Freeport.",
    "Guard Sareken: Go investigate it at once. If you find the orc, destroy it and bring me its cap as proof.",
"You have given away 147 Tunar.",
"You have given away a tough pike scale.",
"You have given away a tough pike scale.",
"You have given away a tough pike scale.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 110, quests[110][2].log)
RemoveTunar(mySession, 147 )
TurnInItem(mySession, items.TOUGH_PIKE_SCALE, 3)
else
    npcDialogue = "Have you returned with the supplies already?"
    diagOptions = { "Yes. I have it all right here.", "Whoops, not yet." }
end
else
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with 147 tunar and three tough pike scales and you will be rewarded."
end
elseif (GetPlayerFlags(mySession, "110") == "3") then
if (CheckQuestItem(mySession, items.ORC_RANSACKERS_CAP, 1))
 then
if (choice:find("don\'t")) then
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with the orc ransacker's cap, which can be found near the old house north of Freeport."
elseif (choice:find("wasn\'t")) then
multiDialogue = { "Guard Sareken: Yes, this does appear to be the cap of such an orc. It is quite alarming that this dangerous creature has wandered this close to the city. I must investigate further.",
    "Guard Sareken: playerName, you have proven yourself worthy as a warrior of The Freeport Militia. Captain Norgam will hear of my highest marks for your performance.",
    "Guard Sareken: As your reward, I offer you this Sturdy Two-Handed Sword. May you use it well as you study the ways of battle.",
    "Guard Sareken: I am certain that with your talent, Captain Norgam will call upon you again.",
"You have given away an orc ransacker's cap.",
"You have received a Sturdy Two-Handed Sword.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 110, quests[110][3].xp, 113 )
TurnInItem(mySession, items.ORC_RANSACKERS_CAP, 1)
GrantItem(mySession, items.STURDY_TWO-HANDED_SWORD, 1)
else
    npcDialogue = "Do you have the cap?"
    diagOptions = { "It wasn't easy, but yes.", "I don't think so..." }
end
else
npcDialogue = "Guard Sareken: I know it is asking a lot, but please consider returning with what I have requested for our cause. Return to me with the orc ransacker's cap, which can be found near the old house north of Freeport."
end
  else
        npcDialogue =
"Guard Sareken: No one realizes how difficult this job actually is, playerName. It's blazing hot, criminals are everywhere, and we don't even have all the supplies to do our work. Some days, I think about changing to a simpler life."
    end
------




SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end



