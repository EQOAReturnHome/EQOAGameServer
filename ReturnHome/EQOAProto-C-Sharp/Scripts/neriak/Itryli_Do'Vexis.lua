function  event_say(choice)
diagOptions = {}
    npcDialogue = "Lady Tzserinia may take kindly to strangers, but I do not. Get away from me."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end