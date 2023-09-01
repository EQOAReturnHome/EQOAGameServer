function  event_say(choice)
diagOptions = {}
    npcDialogue = "I can help clerics clear their mind, if certain conditions are met."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end