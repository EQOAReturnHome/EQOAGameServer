function  event_say(choice)
diagOptions = {}
    npcDialogue = "Nothing feels safer to me than the walls of this hold.  I find it almost impenetrable."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end