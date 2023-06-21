local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Magician(10) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "100115") == "1") then
if (choice:find("thanks")) then
multiDialogue = { "Dagget Klem: More for me then, I suppose."
 } 
elseif (choice:find("nightworm")) then
multiDialogue = { "Dagget Klem: Ah yes, Ilenar. One of my best customers. Those roots are not easy to pull from the swamps. But I happen to have a shipment for sale.",
    "Dagget Klem: The only problem is, these bloody sharks are making it impossible for ships to dock here.",
    "Dagget Klem: It's a new species of shark, they're called bloodfins. And they've been using the coast as a spawning ground.",
    "Dagget Klem: If you could kill a mother bloodfin, I could get a boat through. To lure a mother out, you'll have to kill the smaller bloodfin sharks in great numbers.",
    "Dagget Klem: Once you've killed a bloodfin mother, bring me one of its teeth as proof.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100115, quests[100115][1].log)
else
    npcDialogue = "Interested in some fish?"
    diagOptions = { "Ilenar sent me for nightworm root.", "No thanks, I just ate." }
end
elseif (GetPlayerFlags(mySession, "100115") == "2") then
if (mySession.MyCharacter.Inventory.Tunar >= 260
and CheckQuestItem(mySession, items.BLOODFIN_BROOD_MOTHER_TOOTH, 1))
 then
multiDialogue = { "Dagget Klem: You look a bit doused, but it seems you've managed to take out a mother bloodfin. Impressive! Lets see that tooth...",
    "Dagget Klem: Very nice. Well, I'll be able to get that ship docked here shortly. We will get you that shipment of nightworm roots you asked for."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "You still want to buy some nightworm roots, right?"
    diagOptions = { "Yes. Here is 260 Tunar.", "Not quite yet." }
elseif (choice:find("quite")) then
npcDialogue = "Dagget Klem: Bring these items to me as soon as you possibly can. Once you've killed a bloodfin mother, bring me one of its teeth as proof."
elseif (choice:find("Tunar")) then
multiDialogue = { "Dagget Klem: Ok then. Here they are. Pleasure doing business. Oh, and best not let them spill out of that case. They are quite deadly...",
"You have given away a bloodfin brood mother tooth.",
"You have given away 260 Tunar.",
"You have received the nightworm roots.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 100115, quests[100115][2].log)
TurnInItem(mySession, items.BLOODFIN_BROOD_MOTHER_TOOTH, 1)
RemoveTunar(mySession, 260 )
GrantItem(mySession, items.NIGHTWORM_ROOTS, 1)
else
npcDialogue = "Dagget Klem: Bring these items to me as soon as you possibly can. Once you've killed a bloodfin mother, bring me one of its teeth as proof."
end
  else
        npcDialogue =
"Dagget Klem: If you decide to go for a swim, watch out for those deadly bloodfins. They eat travelers like you for breakfast."
    end
------
--Bard(5) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "50115") == "1") then
if (choice:find("thanks")) then
multiDialogue = { "Dagget Klem: More for me then, I suppose."
 } 
elseif (choice:find("nightworm")) then
multiDialogue = { "Dagget Klem: Ah yes, Ilenar. One of my best customers. Those roots are not easy to pull from the swamps. But I happen to have a shipment for sale.",
    "Dagget Klem: The only problem is, these bloody sharks are making it impossible for ships to dock here.",
    "Dagget Klem: It's a new species of shark, they're called bloodfins. And they've been using the coast as a spawning ground.",
    "Dagget Klem: If you could kill a mother bloodfin, I could get a boat through. To lure a mother out, you'll have to kill the smaller bloodfin sharks in great numbers.",
    "Dagget Klem: Once you've killed a bloodfin mother, bring me one of its teeth as proof.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50115, quests[50115][1].log)
else
    npcDialogue = "Interested in some fish?"
    diagOptions = { "Ilenar sent me for nightworm root.", "No thanks, I just ate." }
end
elseif (GetPlayerFlags(mySession, "50115") == "2") then
if (mySession.MyCharacter.Inventory.Tunar >= 260
and CheckQuestItem(mySession, items.BLOODFIN_BROOD_MOTHER_TOOTH, 1))
 then
multiDialogue = { "Dagget Klem: You look a bit doused, but it seems you've managed to take out a mother bloodfin. Impressive! Lets see that tooth...",
    "Dagget Klem: Very nice. Well, I'll be able to get that ship docked here shortly. We will get you that shipment of nightworm roots you asked for."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "You still want to buy some nightworm roots, right?"
    diagOptions = { "Yes. Here is 260 Tunar.", "Not quite yet." }
elseif (choice:find("quite")) then
npcDialogue = "Dagget Klem: Bring this to me as soon as you possibly can. Once you've killed a bloodfin mother, bring me one of its teeth as proof."
elseif (choice:find("Tunar")) then
multiDialogue = { "Dagget Klem: Ok then. Here they are. Pleasure doing business. Oh, and best not let them spill out of that case. They are quite deadly...",
"You have given away a bloodfin brood mother tooth.",
"You have given away 260 Tunar.",
"You have received the nightworm roots.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 50115, quests[50115][2].log)
TurnInItem(mySession, items.BLOODFIN_BROOD_MOTHER_TOOTH, 1)
RemoveTunar(mySession, 260 )
GrantItem(mySession, items.NIGHTWORM_ROOTS, 1)
else
npcDialogue = "Dagget Klem: Bring this to me as soon as you possibly can. Once you've killed a bloodfin mother, bring me one of its teeth as proof."
end
  else
        npcDialogue =
"Dagget Klem: If you decide to go for a swim, watch out for those deadly bloodfins. They eat travelers like you for breakfast."
    end
------
--Cleric(9) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "90115") == "1") then
if (choice:find("thanks")) then
multiDialogue = { "Dagget Klem: More for me then, I suppose."
 } 
elseif (choice:find("nightworm")) then
multiDialogue = { "Dagget Klem: Ah yes, Ilenar. One of my best customers. Those roots are not easy to pull from the swamps. But I happen to have a shipment for sale.",
    "Dagget Klem: The only problem is, these bloody sharks are making it impossible for ships to dock here.",
    "Dagget Klem: It's a new species of shark, they're called bloodfins. And they've been using the coast as a spawning ground.",
    "Dagget Klem: If you could kill a mother bloodfin, I could get a boat through. To lure a mother out, you may have to kill the smaller bloodfin sharks in great numbers.",
    "Dagget Klem: Once you've killed a bloodfin mother, bring me one of its teeth as proof.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90115, quests[90115][1].log)
else
    npcDialogue = "Interested in some fish?"
    diagOptions = { "Ilenar sent me for nightworm root.", "No thanks, I just ate." }
end
elseif (GetPlayerFlags(mySession, "90115") == "2") then
if (mySession.MyCharacter.Inventory.Tunar >= 260
and CheckQuestItem(mySession, items.BLOODFIN_BROOD_MOTHER_TOOTH, 1))
 then
multiDialogue = { "Dagget Klem: You look a bit doused, but it seems you've managed to take out a mother bloodfin. Impressive! Lets see that tooth...",
    "Dagget Klem: Very nice. Well, I'll be able to get that ship docked here shortly. We will get you that shipment of nightworm roots you asked for."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "You still want to buy some nightworm roots, right?"
    diagOptions = { "Yes. Here is 260 Tunar.", "Not quite yet." }
elseif (choice:find("quite")) then
npcDialogue = "Dagget Klem: Bring these items to me as soon as you possibly can. Once you've killed a bloodfin mother, bring me one of its teeth as proof."
elseif (choice:find("Tunar")) then
multiDialogue = { "Dagget Klem: Ok then. Here they are. Pleasure doing business. Oh, and best not let them spill out of that case. They are quite deadly...",
"You have given away a bloodfin brood mother tooth.",
"You have given away 260 Tunar.",
"You have received the nightworm roots.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 90115, quests[90115][2].log)
TurnInItem(mySession, items.BLOODFIN_BROOD_MOTHER_TOOTH, 1)
RemoveTunar(mySession, 260 )
GrantItem(mySession, items.NIGHTWORM_ROOTS, 1)
else
npcDialogue = "Dagget Klem: Bring these items to me as soon as you possibly can. Once you've killed a bloodfin mother, bring me one of its teeth as proof."
end
  else
        npcDialogue =
"Dagget Klem: If you decide to go for a swim, watch out for those deadly bloodfins. They eat travelers like you for breakfast."
    end
------
--Rogue(6) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "60115") == "1") then
if (choice:find("thanks")) then
multiDialogue = { "Dagget Klem: More for me then, I suppose."
 } 
elseif (choice:find("nightworm")) then
multiDialogue = { "Dagget Klem: Ah yes, Ilenar. One of my best customers. Those roots are not easy to pull from the swamps. But I happen to have a shipment for sale.",
    "Dagget Klem: The only problem is, these bloody sharks are making it impossible for ships to dock here.",
    "Dagget Klem: It's a new species of shark, they're called bloodfins. And they've been using the coast as a spawning ground.",
    "Dagget Klem: If you could kill a mother bloodfin, I could get a boat through. To lure a mother out, you may have to kill the smaller bloodfin sharks in great numbers.",
    "Dagget Klem: Once you've killed a bloodfin mother, bring me one of its teeth as proof.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60115, quests[60115][1].log)
else
    npcDialogue = "Interested in some fish?"
    diagOptions = { "Ilenar sent me for nightworm root.", "No thanks, I just ate." }
end
elseif (GetPlayerFlags(mySession, "60115") == "2") then
if (mySession.MyCharacter.Inventory.Tunar >= 260
and CheckQuestItem(mySession, items.BLOODFIN_BROOD_MOTHER_TOOTH, 1))
 then
multiDialogue = { "Dagget Klem: You look a bit doused, but it seems you've managed to take out a mother bloodfin. Impressive! Lets see that tooth...",
    "Dagget Klem: Very nice. Well, I'll be able to get that ship docked here shortly. We will get you that shipment of nightworm roots you asked for."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "You still want to buy some nightworm roots, right?"
    diagOptions = { "Yes. Here is 260 Tunar.", "Not quite yet." }
elseif (choice:find("quite")) then
npcDialogue = "Dagget Klem: Bring these items to me as soon as you possibly can. Once you've killed a bloodfin mother, bring me one of its teeth as proof."
elseif (choice:find("Tunar")) then
multiDialogue = { "Dagget Klem: Ok then. Here they are. Pleasure doing business. Oh, and best not let them spill out of that case. They are quite deadly...",
"You have given away a bloodfin brood mother tooth.",
"You have given away 260 Tunar.",
"You have received the nightworm roots.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 60115, quests[60115][2].log)
TurnInItem(mySession, items.BLOODFIN_BROOD_MOTHER_TOOTH, 1)
RemoveTunar(mySession, 260 )
GrantItem(mySession, items.NIGHTWORM_ROOTS, 1)
else
npcDialogue = "Dagget Klem: Bring these items to me as soon as you possibly can. Once you've killed a bloodfin mother, bring me one of its teeth as proof."
end
  else
        npcDialogue =
"Dagget Klem: If you decide to go for a swim, watch out for those deadly bloodfins. They eat travelers like you for breakfast."
    end
------
--Warrior(0) Human(0) Eastern(1)
if (GetPlayerFlags(mySession, "115") == "1") then
if (choice:find("thanks")) then
multiDialogue = { "Dagget Klem: More for me then, I suppose."
 } 
elseif (choice:find("nightworm")) then
multiDialogue = { "Dagget Klem: Ah yes, Ilenar. One of my best customers. Those roots are not easy to pull from the swamps. But I happen to have a shipment for sale.",
    "Dagget Klem: The only problem is, these bloody sharks are making it impossible for ships to dock here.",
    "Dagget Klem: It's a new species of shark, they're called bloodfins. And they've been using the coast as a spawning ground.",
    "Dagget Klem: If you could kill a mother bloodfin, I could get a boat through. To lure a mother out, you may have to kill the smaller bloodfin sharks in great numbers.",
    "Dagget Klem: Once you've killed a bloodfin mother, bring me one of its teeth as proof.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 115, quests[115][1].log)
else
    npcDialogue = "Interested in some fish?"
    diagOptions = { "Ilenar sent me for nightworm root.", "No thanks, I just ate." }
end
elseif (GetPlayerFlags(mySession, "115") == "2") then
if (mySession.MyCharacter.Inventory.Tunar >= 260
and CheckQuestItem(mySession, items.BLOODFIN_BROOD_MOTHER_TOOTH, 1))
 then
multiDialogue = { "Dagget Klem: You look a bit doused, but it seems you've managed to take out a mother bloodfin. Impressive! Lets see that tooth...",
    "Dagget Klem: Very nice. Well, I'll be able to get that ship docked here shortly. We will get you that shipment of nightworm roots you asked for."
}
SendMultiDialogue(mySession, multiDialogue)
    npcDialogue = "You still want to buy some nightworm roots, right?"
    diagOptions = { "Yes. Here is 260 Tunar.", "Not quite yet." }
elseif (choice:find("quite")) then
npcDialogue = "Dagget Klem: Bring these items to me as soon as you possibly can. Once you've killed a bloodfin mother, bring me one of its teeth as proof."
elseif (choice:find("Tunar")) then
multiDialogue = { "Dagget Klem: Ok then. Here they are. Pleasure doing business. Oh, and best not let them spill out of that case. They are quite deadly...",
"You have given away a bloodfin brood mother tooth.",
"You have given away 260 Tunar.",
"You have received the nightworm roots.",
    "You have finished a quest!",
    "You have received a quest!"
}
ContinueQuest(mySession, 115, quests[115][2].log)
TurnInItem(mySession, items.BLOODFIN_BROOD_MOTHER_TOOTH, 1)
RemoveTunar(mySession, 260 )
GrantItem(mySession, items.NIGHTWORM_ROOTS, 1)
else
npcDialogue = "Dagget Klem: Bring these items to me as soon as you possibly can. Once you've killed a bloodfin mother, bring me one of its teeth as proof."
end
  else
        npcDialogue =
"Dagget Klem: If you decide to go for a swim, watch out for those deadly bloodfins. They eat travelers like you for breakfast."
    end
------



SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end



