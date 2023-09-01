function  event_say(choice)
diagOptions = {}
    npcDialogue = "State you business or leave. I am sick of you travelers."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end