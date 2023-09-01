function  event_say(choice)
diagOptions = {}
    npcDialogue = "Can't you see I'm on duty? I haven't the time to be wasted with one such as yourself. Away with you!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end