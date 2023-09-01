function  event_say(choice)
diagOptions = {}
    npcDialogue = "I refuse to be seen chatting with the likes of you.  Get away from me."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end