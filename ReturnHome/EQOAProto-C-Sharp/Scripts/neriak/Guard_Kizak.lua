function  event_say(choice)
diagOptions = {}
    npcDialogue = "You enter the house of the Warriors, playerName. I advise you to watch yourself on these grounds."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end