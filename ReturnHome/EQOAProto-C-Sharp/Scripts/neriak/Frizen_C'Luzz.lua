function  event_say(choice)
diagOptions = {}
    npcDialogue = "I have better things to do with my time than waste it with the likes of you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end