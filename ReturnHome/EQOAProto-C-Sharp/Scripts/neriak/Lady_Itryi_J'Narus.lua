function  event_say(choice)
diagOptions = {}
    npcDialogue = "Days on end, I wait for my beloved to come home. He's so busy these days. It's a never ending task, being the Lord and Lady of a respected house in Neriak."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end