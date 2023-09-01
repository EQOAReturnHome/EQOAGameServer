function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm worried about my farm. I don't think the deputies can protect my stock any longer. I may have to move to the farms near Qeynos to find work."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end