function  event_say(choice)
diagOptions = {}
    npcDialogue = "Keeping these gears maintained is a full time job. Please, don't distract me."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end