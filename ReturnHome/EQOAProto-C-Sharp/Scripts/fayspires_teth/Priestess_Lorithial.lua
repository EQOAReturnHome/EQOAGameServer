function event_say()
diagOptions = {}
    npcDialogue = "Tunare, the Mother of All, was amongst the first of the gods to come to Norrath. She claimed the land, and then created the elves. She also created many animals, plants and mystical creatures. Our goddess represents all that is light and good in this world. Because of this, the dark forces continue to assault her and her children, including elves. Through our faith in Tunare shall we survive here, until the day has come that we should cross the Ocean of Tears to the east, and join our fellow elven kind there."
SendDialogue(mySession, npcDialogue, diagOptions)
end