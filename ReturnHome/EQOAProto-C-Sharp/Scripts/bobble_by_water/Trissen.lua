function  event_say(choice)
diagOptions = {}
    npcDialogue = "No, I'm not sharing this antidote. I need it for myself, don't you understand? OH, BE OFF WITH YOU!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end