local quests = require('Scripts/FreeportQuests')
local items = require('Scripts/items')
local diagOptions = {}
local questText = ""
local npcDialogue = ""
local questRewards = {}
local multiDialogue = {}
function  event_say(choice)
--Warrior(0) Human(0) Eastern(1)
if
            (class == "Warrior" and race == "Human" and humanType == "Eastern" and
            GetPlayerFlags(mySession, "101") == "noFlags")
then
        SetPlayerFlags(mySession, "101", "0")
end
if (GetPlayerFlags(mySession, "101") == "0") then
if (choice:find("Freeport")) then
multiDialogue = { "Commander Nothard: Oh do you? Taking on the role of a warrior is no easy task. You are the first to attack, and last to retreat. You must defend others with your very life.",
    "Commander Nothard: But if you insist, I will expect you to complete a number of tasks before you will earn the rank of warrior.",
    "Commander Nothard: Your first task is to acquire a militia bracer from Merchant Galosh. Your fee for this will be waived.",
    "Commander Nothard: When you have the militia bracer, return to me and I'll send you on your second task...If you can manage.",
    "You have received a quest!"
}
StartQuest(mySession, 101, quests[101][0].log)
else
    npcDialogue = "What business do you have here, citizen?"
    diagOptions = { "I wish to be a warrior of The Freeport Militia." }
end
elseif (GetPlayerFlags(mySession, "101") == "1") then
if (CheckQuestItem(mySession, items.MILITIA_BRACER, 1))
 then
if (choice:find("afraid")) then
npcDialogue = "Commander Nothard: I'll need you to purchase the militia bracer from Merchant Galosh, then return to me."
elseif (choice:find("right")) then
multiDialogue = { "Commander Nothard: It's going to take more than running errands to fill this job. We are going to have to test your strength and your wit...",
    "Commander Nothard: I will have your next task ready in a few moments. Don't wander off now...",
    "You have finished a quest!"
}
CompleteQuest(mySession, 101, quests[101][1].xp, 102 )
else
    npcDialogue = "You had better have the bracer I sent you for."
    diagOptions = { "It is right here.", "I'm afraid not." }
end
else
npcDialogue = "Commander Nothard: I'll need you to purchase the militia bracer from Merchant Galosh, then return to me."
end
elseif (GetPlayerFlags(mySession, "102") == "0") then
if (choice:find("Yes")) then
multiDialogue = { "Commander Nothard: Warriors must face the most powerful enemies without fear. They are the frontline defense, and the protectors.",
    "Commander Nothard: As a warrior you must stay between the enemy and the rest of the party.",
    "Commander Nothard: You must control the enemies focus, and when things go wrong, you will spill their own blood so that others may survive.",
    "Commander Nothard: As a warrior, you must put others lives before your own. I will expect nothing less from you.",
"Commander Nothard: Now listen carefully. I need you to speak to Spiritmaster Zole.",
"Commander Nothard: You can find him just outside this building. Return only when you complete any tasks he gives you.",
    "You have received a quest!"
}
StartQuest(mySession, 102, quests[102][0].log)
else
    npcDialogue = "Shall we get started with your next task?"
    diagOptions = { "Yes sir." }
end
elseif (GetPlayerFlags(mySession, "102") == "3") then
if (choice:find("Sorry")) then
multiDialogue = { "Commander Nothard: You'll find that we guildmasters don't like to be kept waiting. I suggest you tend to the task at hand. Consult your quest log if you have lost track of your tasks."
 } 
elseif (choice:find("done")) then
multiDialogue = { "Commander Nothard: Excellent. Be sure to have yourself bound often. It is quite inconvenient to be defeated far from your last binding.",
    "Commander Nothard: Now that you've completed that, I have another task for you. Go see Capitan Norgam, he will assist you.",
    "Commander Nothard: You can find Capitan Norgam downstairs, in the north wing of this building.",
    "You have finished a quest!"
}
CompleteQuest(mySession, 102, quests[102][3].xp, 103 )
else
    npcDialogue = "Have you executed the orders I have given you?"
    diagOptions = { "Yes, it is done.", "Sorry sir, no." }
end
  else
        npcDialogue =
"Commander Nothard: A warrior must control the enemies focus, and when that fails, they spill their own blood so that others may survive. They put others lives before their own. I will expect nothing less from the warriors trained here at the Freeport Militia."
    end
------
SendDialogue(mySession, npcDialogue, diagOptions)
SendMultiDialogue(mySession, multiDialogue)
end

