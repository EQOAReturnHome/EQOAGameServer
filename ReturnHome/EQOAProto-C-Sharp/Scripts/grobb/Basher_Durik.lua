function  event_say(choice)
diagOptions = {}
    npcDialogue = "Welcome to troll city of Grobb. Warlord Jurglash will see to your quick death if he finds you even remotely displeasing. Hope you enjoy your visit, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end