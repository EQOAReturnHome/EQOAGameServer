function  event_say(choice)
diagOptions = {}
    npcDialogue = "The only thing more disgusting than a weakling, like you playerName, is a dirty Lycan. Kill them all I say."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end