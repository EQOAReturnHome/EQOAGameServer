function  event_say(choice)
diagOptions = {}
    npcDialogue = "I've had to go clear out that orc camp west of here several times already. I am getting concerned about the safety of this quiet town."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end