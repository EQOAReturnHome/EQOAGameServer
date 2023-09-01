function  event_say(choice)
diagOptions = {}
    npcDialogue = "I certainly hope you've no intention of causing a commotion."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end