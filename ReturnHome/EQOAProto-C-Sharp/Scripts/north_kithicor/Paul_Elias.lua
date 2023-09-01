function event_say()
diagOptions = {}
    npcDialogue = "Dena and I sell our particular set of skills to travelers that visit the inn. I don't judge the job or targets. Whatever it takes to make a decent living in this isolated place."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end