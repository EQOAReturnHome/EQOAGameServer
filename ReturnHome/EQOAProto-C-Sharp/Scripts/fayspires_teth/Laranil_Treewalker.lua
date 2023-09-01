function event_say()
diagOptions = {}
    npcDialogue = "You can find our city craft in various places on Tunaria. There is an elf village just north along the coast. Far to the west, in a forest near the coastline lives a band of elves. Their home is known as Mariel Village."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end