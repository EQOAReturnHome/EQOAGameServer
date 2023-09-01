function  event_say(choice)
diagOptions = {}
    npcDialogue = "We will protect this city with our lives if it's the last thing we do."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end