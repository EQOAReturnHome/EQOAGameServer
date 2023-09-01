function  event_say(choice)
diagOptions = {}
    npcDialogue = "I have no use for the likes of you. I'm sure you can find someone else to bother."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end