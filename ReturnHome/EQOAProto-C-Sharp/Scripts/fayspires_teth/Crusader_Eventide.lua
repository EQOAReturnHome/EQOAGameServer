function event_say()
diagOptions = {}
    npcDialogue = "I will fight with my last breath to save our people, but if I am honest, I can't help but feel as though this city is doomed. I often have visions of Fayspires sunk to the bottom of the lake. I think our time here is limited, and we should make our way to the land across the ocean in the east..."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end