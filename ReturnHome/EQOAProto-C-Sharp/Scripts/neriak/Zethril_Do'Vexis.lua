function  event_say(choice)
diagOptions = {}
    npcDialogue = "You dare enter my chambers unannounced?!! I suggest you leave this place before my blade drinks of your blood."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end