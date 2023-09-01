function  event_say(choice)
diagOptions = {}
    npcDialogue = "Gilsop and I are the first line of defense. Twenty years and we haven't let one vile greenskin through this tunnel."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end