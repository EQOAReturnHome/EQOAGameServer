function  event_say(choice)
diagOptions = {}
    npcDialogue = "Shhh! Leave me alone! I don't want to be associated with the likes of you!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end