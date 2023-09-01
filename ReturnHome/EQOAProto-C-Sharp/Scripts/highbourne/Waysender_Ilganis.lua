function event_say()
diagOptions = {}
    npcDialogue = "I have figured out how to transport explorers to Klick'Anon but it doesn't always work. I guess you must meet some very specific conditions."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end