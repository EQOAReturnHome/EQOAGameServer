function  event_say(choice)
diagOptions = {}
    npcDialogue = "Let us know if you see any o' them thievin' dark elves prowlin' around here."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end