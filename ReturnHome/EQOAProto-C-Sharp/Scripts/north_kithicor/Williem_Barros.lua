function event_say()
diagOptions = {}
    npcDialogue = "These majestic creatures are our best defense against the forest and foes that surround us. They can see clearly in night and day, pick up scents from miles away, and those front talons are unforgiving."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end