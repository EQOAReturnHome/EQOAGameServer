function  event_say(choice)
diagOptions = {}
    npcDialogue = "Is there a meaning to this intrusion?! Get out now before I have your head on a stake!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end