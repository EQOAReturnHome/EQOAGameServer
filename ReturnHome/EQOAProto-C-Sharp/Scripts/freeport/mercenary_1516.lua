function  event_say(choice)
diagOptions = {}
    npcDialogue = "No one gets into the Shining Shield unless we let them. Including you, playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end