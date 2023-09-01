function event_say()
diagOptions = {}
    npcDialogue = "I started living here about a year ago. Though it's a small village, there is something magical about it. I do prefer this to that big city life. It's so peaceful, and connected to the present."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end