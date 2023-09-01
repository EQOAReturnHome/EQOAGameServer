function event_say()
diagOptions = {}
    npcDialogue = "I have stationed myself here at the North gate to watch the comings and goings of our city. I believe that it is in adventurers, such as yourself, that salvation and advancement can be found."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end