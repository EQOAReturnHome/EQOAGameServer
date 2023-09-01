function event_say()
diagOptions = {}
    npcDialogue = "If I only succeed in teaching you one thing, let it be that you must bond with your elementals. They will be there for you in the most dire of circumstances when, more often than not, others won't."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end