function  event_say(choice)
diagOptions = {}
    npcDialogue = "We've had a problem with the wolves near town for some time, but it's been getting out of hand lately."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end