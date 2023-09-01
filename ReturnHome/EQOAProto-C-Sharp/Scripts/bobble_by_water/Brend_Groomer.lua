function  event_say(choice)
diagOptions = {}
    npcDialogue = "Greetings! We'll be having no trouble from you in Bobble-by-Water, you understand?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end