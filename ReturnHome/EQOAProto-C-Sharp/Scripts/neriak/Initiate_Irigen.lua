function  event_say(choice)
diagOptions = {}
    npcDialogue = "Step out of the arena, please. I aim to stop Sardun's winning streak."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end