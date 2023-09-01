function event_say()
diagOptions = {}
    npcDialogue = "There is no other place I would rather be. Here amongst trees, with my people, defending our city from the evils of the world. I am home!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end