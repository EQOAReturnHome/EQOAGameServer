function  event_say(choice)
diagOptions = {}
    npcDialogue = "Give me some time to work on this spell. I may eventually be able to whisk you away to far away places."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end