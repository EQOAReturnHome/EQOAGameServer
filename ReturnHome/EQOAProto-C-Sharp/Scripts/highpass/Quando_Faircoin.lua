function  event_say(choice)
diagOptions = {}
    npcDialogue = "Take a seat at the pews and feel free to rest yourself.  These walls are open to all who are in need."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end