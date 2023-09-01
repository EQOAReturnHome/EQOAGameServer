function  event_say(choice)
diagOptions = {}
    npcDialogue = "If you witness any villainy, be sure to report it immediately. We do not tolerate transgression."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end