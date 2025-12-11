PRAGMA foreign_keys = ON;

BEGIN TRANSACTION;

CREATE TABLE IF NOT EXISTS user (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  username TEXT NOT NULL UNIQUE,
  password TEXT NOT NULL,
  isModerator INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS artist (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  name TEXT NOT NULL,
  description TEXT,
  beginDate TEXT NOT NULL,
  endDate TEXT,
  location TEXT,
  isApproved INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS genre (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  name TEXT NOT NULL,
  description TEXT,
  isApproved INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS "release" (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  title TEXT NOT NULL,
  description TEXT,
  releaseDate TEXT NOT NULL,
  artistId INTEGER NOT NULL,
  genreId INTEGER,
  isApproved INTEGER NOT NULL DEFAULT 0,
  FOREIGN KEY (artistId) REFERENCES artist(id) ON DELETE CASCADE,
  FOREIGN KEY (genreId) REFERENCES genre(id) ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS track (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  title TEXT NOT NULL,
  description TEXT,
  duration INTEGER NOT NULL,
  releaseId INTEGER NOT NULL,
  isApproved INTEGER NOT NULL DEFAULT 0,
  FOREIGN KEY (releaseId) REFERENCES "release"(id) ON DELETE CASCADE
);

INSERT OR IGNORE INTO user (username, password, isModerator)VALUES
('user', 'user', 0),
('admin', 'admin', 1);

INSERT INTO genre (name, description, isApproved) VALUES
('Alternative Rock', 'Broad alternative rock genre', 1),
('Indie Rock', 'Independent and underground rock music', 1),
('Metal', 'Heavy and extreme metal music', 1),
('Electronic', 'Electronic and experimental music', 1),
('Trip Hop', 'Downtempo electronic music', 1),
('Shoegaze', 'Atmospheric and effects-heavy rock', 1),
('Post-Hardcore', 'Aggressive and experimental hardcore punk', 1),
('Ambient', 'Atmospheric and cinematic soundscapes', 1),
('Hardcore', 'Aggressive punk rock genre', 0);

INSERT INTO artist (name, description, beginDate, location, isApproved) VALUES
('Elliott Smith', 'Singer-songwriter known for intimate folk songs', '1991-01-01', 'USA', 1),
('Deftones', 'Alternative metal band from Sacramento', '1988-01-01', 'USA', 1),
('PJ Harvey', 'English singer-songwriter and poet', '1991-01-01', 'UK', 1),
('Meshuggah', 'Progressive extreme metal band', '1987-01-01', 'Sweden', 1),
('Fiona Apple', 'Art pop singer-songwriter', '1996-01-01', 'USA', 1),
('Radiohead', 'Experimental alternative rock band', '1985-01-01', 'UK', 1),
('Aphex Twin', 'Pioneering electronic musician', '1991-01-01', 'UK', 1),
('Jeff Buckley', 'Singer-songwriter with a powerful vocal range', '1990-01-01', 'USA', 1),
('Portishead', 'Trip hop pioneers', '1991-01-01', 'UK', 1),
('Tool', 'Progressive metal band', '1990-01-01', 'USA', 1),
('Converge', 'Influential metalcore and hardcore band', '1990-02-01', 'USA', 0);


INSERT INTO "release" (title, description, releaseDate, artistId, genreId, isApproved) VALUES
('Either/Or', 'Acoustic and lo-fi folk album', '1997-02-25', 1, 2, 1),
('Roman Candle', 'Lo-fi debut album recorded on four-track', '1994-07-14', 1, 2, 1),
('XO', 'Lushly produced singer-songwriter album', '1998-08-25', 1, 2, 1),

('White Pony', 'Genre-defining alternative metal album', '2000-06-20', 2, 3, 1),
('Around the Fur', 'Aggressive and influential nu metal album', '1997-10-28', 2, 3, 1),
('Diamond Eyes', 'Atmospheric and melodic metal record', '2010-05-04', 2, 3, 1),

('Rid of Me', 'Raw and minimalist rock album', '1993-05-04', 3, 1, 1),
('Dry', 'Raw debut featuring stripped-down arrangements', '1992-03-30', 3, 1, 1),
('To Bring You My Love', 'Blues-infused and dramatic album', '1995-02-27', 3, 1, 1),

('Nothing', 'Polyrhythmic and aggressive metal', '2002-07-22', 4, 3, 1),
('Chaosphere', 'Fast and chaotic extreme metal', '1998-11-09', 4, 3, 1),
('obZen', 'Polished and crushing modern metal', '2008-03-07', 4, 3, 1),

('When the Pawn...', 'Complex art pop album', '1999-11-09', 5, 2, 1),
('Tidal', 'Debut album featuring piano-driven songs', '1996-07-23', 5, 2, 1),
('The Idler Wheel...', 'Percussion-heavy minimalist album', '2012-06-15', 5, 2, 1),

('OK Computer', 'Landmark experimental rock album', '1997-05-21', 6, 1, 1),
('The Bends', 'Emotionally charged guitar rock album', '1995-03-13', 6, 1, 1),
('Kid A', 'Electronic and experimental departure', '2000-10-02', 6, 4, 1),

('Selected Ambient Works 85-92', 'Influential ambient techno album', '1992-11-09', 7, 8, 1),
('Richard D. James Album', 'Playful and abrasive electronic record', '1996-11-04', 7, 4, 1),
('Drukqs', 'Complex and fragmented electronic compositions', '2001-10-22', 7, 4, 1),

('Grace', 'Only studio album by Jeff Buckley', '1994-08-23', 8, 2, 1),
('Sketches for My Sweetheart the Drunk', 'Posthumous collection of studio and demo recordings', '1998-05-26', 8, 2, 1),

('Dummy', 'Trip hop classic', '1994-08-22', 9, 5, 1),
('Portishead', 'Dark and abrasive follow-up to Dummy', '1997-08-18', 9, 5, 1),
('Third', 'Experimental and unsettling album', '2008-04-25', 9, 5, 1),

('Lateralus', 'Progressive metal masterpiece', '2001-05-15', 10, 3, 1),
('Aenima', 'Dark and complex progressive metal album', '1996-09-17', 10, 3, 1),
('10,000 Days', 'Dense and spiritual progressive metal', '2006-05-02', 10, 3, 1),

('Jane Doe', 'Seminal metallic hardcore album', '2001-09-04', 11, 9, 0);


INSERT INTO track (title, description, duration, releaseId, isApproved) VALUES
-- Either/Or
('Between the Bars', 'Soft acoustic song', 172, (SELECT id FROM "release" WHERE title='Either/Or'), 1),
('Angeles', 'Fingerpicked guitar track', 170, (SELECT id FROM "release" WHERE title='Either/Or'), 1),
('Speed Trials', 'Opening track with upbeat melody', 216, (SELECT id FROM "release" WHERE title='Either/Or'), 1),
('Rose Parade', 'Bittersweet acoustic song', 193, (SELECT id FROM "release" WHERE title='Either/Or'), 1),
('Say Yes', 'Optimistic closing track', 140, (SELECT id FROM "release" WHERE title='Either/Or'), 1),

-- White Pony
('Digital Bath', 'Atmospheric and dark', 255, (SELECT id FROM "release" WHERE title='White Pony'), 1),
('Change (In the House of Flies)', 'Moody and iconic track', 299, (SELECT id FROM "release" WHERE title='White Pony'), 1),
('Feiticeira', 'Heavy and groove-driven opener', 199, (SELECT id FROM "release" WHERE title='White Pony'), 1),
('Elite', 'Aggressive and chaotic track', 237, (SELECT id FROM "release" WHERE title='White Pony'), 1),
('Knife Prty', 'Cinematic and dark atmosphere', 290, (SELECT id FROM "release" WHERE title='White Pony'), 1),

-- Rid of Me
('Rid of Me', 'Aggressive vocal performance', 243, (SELECT id FROM "release" WHERE title='Rid of Me'), 1),
('50ft Queenie', 'Short and explosive punk track', 143, (SELECT id FROM "release" WHERE title='Rid of Me'), 1),
('Yuri-G', 'Minimalist guitar-driven song', 226, (SELECT id FROM "release" WHERE title='Rid of Me'), 1),
('Dry', 'Re-recorded version with raw energy', 263, (SELECT id FROM "release" WHERE title='Rid of Me'), 1),

-- Nothing
('Stengah', 'Complex rhythmic structures', 286, (SELECT id FROM "release" WHERE title='Nothing'), 1),
('Perpetual Black Second', 'Dissonant and heavy', 302, (SELECT id FROM "release" WHERE title='Nothing'), 1),
('Rational Gaze', 'Iconic Meshuggah rhythm work', 256, (SELECT id FROM "release" WHERE title='Nothing'), 1),
('Closed Eye Visuals', 'Mechanical and relentless', 298, (SELECT id FROM "release" WHERE title='Nothing'), 1),

-- When the Pawn...
('Fast As You Can', 'Energetic piano-driven song', 238, (SELECT id FROM "release" WHERE title='When the Pawn...'), 1),
('Limp', 'Bold and confident piano performance', 232, (SELECT id FROM "release" WHERE title='When the Pawn...'), 1),
('Paper Bag', 'Jazz-influenced rhythm and vocals', 220, (SELECT id FROM "release" WHERE title='When the Pawn...'), 1),

-- OK Computer
('Paranoid Android', 'Multi-part experimental song', 386, (SELECT id FROM "release" WHERE title='OK Computer'), 1),
('No Surprises', 'Melancholic and melodic', 228, (SELECT id FROM "release" WHERE title='OK Computer'), 1),
('Airbag', 'Electronic-infused rock opener', 284, (SELECT id FROM "release" WHERE title='OK Computer'), 1),
('Karma Police', 'Satirical and melodic track', 261, (SELECT id FROM "release" WHERE title='OK Computer'), 1),
('Lucky', 'Uplifting yet anxious tone', 259, (SELECT id FROM "release" WHERE title='OK Computer'), 1),

-- Selected Ambient Works 85-92
('Xtal', 'Warm ambient techno', 280, (SELECT id FROM "release" WHERE title='Selected Ambient Works 85-92'), 1),
('Pulsewidth', 'Melodic and analog-heavy', 223, (SELECT id FROM "release" WHERE title='Selected Ambient Works 85-92'), 1),
('Heliosphan', 'Bright and energetic synth track', 308, (SELECT id FROM "release" WHERE title='Selected Ambient Works 85-92'), 1),

-- Grace
('Grace', 'Dynamic vocal performance', 321, (SELECT id FROM "release" WHERE title='Grace'), 1),
('Hallelujah', 'Cover with emotional intensity', 396, (SELECT id FROM "release" WHERE title='Grace'), 1),
('Mojo Pin', 'Emotionally intense opener', 334, (SELECT id FROM "release" WHERE title='Grace'), 1),
('Last Goodbye', 'Melodic and accessible', 285, (SELECT id FROM "release" WHERE title='Grace'), 1),
('Dream Brother', 'Dark and haunting atmosphere', 315, (SELECT id FROM "release" WHERE title='Grace'), 1),

-- Dummy
('Sour Times', 'Downtempo trip hop classic', 251, (SELECT id FROM "release" WHERE title='Dummy'), 1),
('Mysterons', 'Cinematic trip-hop opener', 300, (SELECT id FROM "release" WHERE title='Dummy'), 1),
('Glory Box', 'Sultry and blues-inspired', 305, (SELECT id FROM "release" WHERE title='Dummy'), 1),

-- Lateralus
('Schism', 'Odd time signature showcase', 406, (SELECT id FROM "release" WHERE title='Lateralus'), 1),
('The Grudge', 'Aggressive progressive opener', 516, (SELECT id FROM "release" WHERE title='Lateralus'), 1),
('Parabola', 'Spiritual and heavy centerpiece', 368, (SELECT id FROM "release" WHERE title='Lateralus'), 1),
('Ticks & Leeches', 'Extreme and intense performance', 508, (SELECT id FROM "release" WHERE title='Lateralus'), 1),

-- Roman Candle
('Roman Candle', 'Raw lo-fi opener', 222, (SELECT id FROM "release" WHERE title='Roman Candle'), 1),
('Condor Ave', 'Narrative-driven folk song', 225, (SELECT id FROM "release" WHERE title='Roman Candle'), 1),
('No Name #1', 'Minimal acoustic track', 200, (SELECT id FROM "release" WHERE title='Roman Candle'), 1),
('Last Call', 'Melancholic closing track', 264, (SELECT id FROM "release" WHERE title='Roman Candle'), 1),

-- XO
('Sweet Adeline', 'Bright melodic opener', 215, (SELECT id FROM "release" WHERE title='XO'), 1),
('Waltz #2 (XO)', 'Iconic piano-driven song', 295, (SELECT id FROM "release" WHERE title='XO'), 1),
('Baby Britain', 'Bittersweet pop track', 250, (SELECT id FROM "release" WHERE title='XO'), 1),
('Tomorrow Tomorrow', 'Grand orchestral closer', 330, (SELECT id FROM "release" WHERE title='XO'), 1),

-- Around the Fur
('My Own Summer (Shove It)', 'Explosive riff-driven track', 215, (SELECT id FROM "release" WHERE title='Around the Fur'), 1),
('Lhabia', 'Chaotic and heavy', 247, (SELECT id FROM "release" WHERE title='Around the Fur'), 1),
('Mascara', 'Moody and atmospheric', 225, (SELECT id FROM "release" WHERE title='Around the Fur'), 1),
('Around the Fur', 'Aggressive title track', 190, (SELECT id FROM "release" WHERE title='Around the Fur'), 1),

-- Diamond Eyes
('Diamond Eyes', 'High-energy opening track', 188, (SELECT id FROM "release" WHERE title='Diamond Eyes'), 1),
('Royal', 'Short and punishing', 214, (SELECT id FROM "release" WHERE title='Diamond Eyes'), 1),
('Sextape', 'Dreamy and melodic', 242, (SELECT id FROM "release" WHERE title='Diamond Eyes'), 1),
('976-EVIL', 'Ethereal and dark', 257, (SELECT id FROM "release" WHERE title='Diamond Eyes'), 1),

-- Dry
('Oh My Lover', 'Dark and intense debut track', 270, (SELECT id FROM "release" WHERE title='Dry'), 1),
('O Stella', 'Anger and vulnerability mixed', 215, (SELECT id FROM "release" WHERE title='Dry'), 1),
('Sheela-Na-Gig', 'Feminist punk anthem', 195, (SELECT id FROM "release" WHERE title='Dry'), 1),
('Joe', 'Melodic and aggressive closer', 290, (SELECT id FROM "release" WHERE title='Dry'), 1),

-- To Bring You My Love
('To Bring You My Love', 'Haunting blues opener', 255, (SELECT id FROM "release" WHERE title='To Bring You My Love'), 1),
('Down by the Water', 'Menacing and iconic single', 214, (SELECT id FROM "release" WHERE title='To Bring You My Love'), 1),
('Cmon Billy', 'Emotional and restrained', 180, (SELECT id FROM "release" WHERE title='To Bring You My Love'), 1),
('Send His Love to Me', 'Noir-inspired rock track', 205, (SELECT id FROM "release" WHERE title='To Bring You My Love'), 1),

-- Chaosphere
('Concatenation', 'Relentless opening assault', 222, (SELECT id FROM "release" WHERE title='Chaosphere'), 1),
('New Millennium Cyanide Christ', 'Mechanical and brutal', 310, (SELECT id FROM "release" WHERE title='Chaosphere'), 1),
('Corridor of Chameleons', 'Dissonant rhythms', 245, (SELECT id FROM "release" WHERE title='Chaosphere'), 1),
('Elastic', 'Unpredictable structures', 300, (SELECT id FROM "release" WHERE title='Chaosphere'), 1),

-- obZen
('Combustion', 'Explosive album opener', 280, (SELECT id FROM "release" WHERE title='obZen'), 1),
('Bleed', 'Iconic polyrhythmic track', 442, (SELECT id FROM "release" WHERE title='obZen'), 1),
('Lethargica', 'Slow and crushing', 355, (SELECT id FROM "release" WHERE title='obZen'), 1),
('Pravus', 'Dark and complex closer', 290, (SELECT id FROM "release" WHERE title='obZen'), 1),

-- Tidal
('Sleep to Dream', 'Confident debut opener', 232, (SELECT id FROM "release" WHERE title='Tidal'), 1),
('Shadowboxer', 'Jazz-influenced pop', 250, (SELECT id FROM "release" WHERE title='Tidal'), 1),
('Criminal', 'Sultry and minimal', 221, (SELECT id FROM "release" WHERE title='Tidal'), 1),
('Carrion', 'Emotional piano ballad', 270, (SELECT id FROM "release" WHERE title='Tidal'), 1),

-- The Idler Wheel...
('Every Single Night', 'Percussion-driven opener', 215, (SELECT id FROM "release" WHERE title='The Idler Wheel...'), 1),
('Werewolf', 'Sharp and playful', 190, (SELECT id FROM "release" WHERE title='The Idler Wheel...'), 1),
('Hot Knife', 'Rhythmic and hypnotic', 230, (SELECT id FROM "release" WHERE title='The Idler Wheel...'), 1),
('Valentine', 'Vulnerable closing moment', 225, (SELECT id FROM "release" WHERE title='The Idler Wheel...'), 1),

-- The Bends
('Planet Telex', 'Atmospheric opener', 265, (SELECT id FROM "release" WHERE title='The Bends'), 1),
('The Bends', 'Soaring guitar-driven track', 245, (SELECT id FROM "release" WHERE title='The Bends'), 1),
('Fake Plastic Trees', 'Emotional centerpiece', 290, (SELECT id FROM "release" WHERE title='The Bends'), 1),
('Street Spirit (Fade Out)', 'Iconic and haunting closer', 255, (SELECT id FROM "release" WHERE title='The Bends'), 1),

-- Kid A
('Everything in Its Right Place', 'Electronic album opener', 250, (SELECT id FROM "release" WHERE title='Kid A'), 1),
('Kid A', 'Abstract and unsettling', 275, (SELECT id FROM "release" WHERE title='Kid A'), 1),
('How to Disappear Completely', 'Orchestral and emotional', 355, (SELECT id FROM "release" WHERE title='Kid A'), 1),
('Idioteque', 'Anxious electronic pulse', 305, (SELECT id FROM "release" WHERE title='Kid A'), 1),

-- Richard D. James Album
('4', 'Fast and playful opener', 290, (SELECT id FROM "release" WHERE title='Richard D. James Album'), 1),
('Cornish Acid', 'Classic acid techno', 300, (SELECT id FROM "release" WHERE title='Richard D. James Album'), 1),
('Girl/Boy Song', 'Drum-heavy melodic track', 250, (SELECT id FROM "release" WHERE title='Richard D. James Album'), 1),
('Fingerbib', 'Warm and melodic closer', 280, (SELECT id FROM "release" WHERE title='Richard D. James Album'), 1),

-- Drukqs
('Vordhosbn', 'Breakneck rhythm programming', 240, (SELECT id FROM "release" WHERE title='Drukqs'), 1),
('Cock/Ver10', 'Aggressive electronic track', 210, (SELECT id FROM "release" WHERE title='Drukqs'), 1),
('Avril 14th', 'Minimal piano composition', 125, (SELECT id FROM "release" WHERE title='Drukqs'), 1),
('Mt Saint Michel + Saint Michaels Mount', 'Chaotic drum workout', 470, (SELECT id FROM "release" WHERE title='Drukqs'), 1),

-- Sketches for My Sweetheart the Drunk
('Everybody Here Wants You', 'Smooth and soulful', 290, (SELECT id FROM "release" WHERE title='Sketches for My Sweetheart the Drunk'), 1),
('Nightmares by the Sea', 'Dark acoustic blues', 260, (SELECT id FROM "release" WHERE title='Sketches for My Sweetheart the Drunk'), 1),
('Vancouver', 'Unfinished but emotional', 230, (SELECT id FROM "release" WHERE title='Sketches for My Sweetheart the Drunk'), 1),
('Jewel Box', 'Sparse and haunting demo', 240, (SELECT id FROM "release" WHERE title='Sketches for My Sweetheart the Drunk'), 1),

-- Portishead
('Cowboys', 'Dark and abrasive opener', 265, (SELECT id FROM "release" WHERE title='Portishead'), 1),
('All Mine', 'Cinematic ballad', 240, (SELECT id FROM "release" WHERE title='Portishead'), 1),
('Over', 'Slow-burning tension', 275, (SELECT id FROM "release" WHERE title='Portishead'), 1),
('Elysium', 'Minimalist and cold closer', 310, (SELECT id FROM "release" WHERE title='Portishead'), 1),

-- Third
('Silence', 'Brooding album opener', 285, (SELECT id FROM "release" WHERE title='Third'), 1),
('The Rip', 'Builds from calm to chaos', 300, (SELECT id FROM "release" WHERE title='Third'), 1),
('Machine Gun', 'Industrial and repetitive', 285, (SELECT id FROM "release" WHERE title='Third'), 1),
('Threads', 'Atmospheric closer', 315, (SELECT id FROM "release" WHERE title='Third'), 1),

-- Aenima
('Stinkfist', 'Groove-heavy opener', 312, (SELECT id FROM "release" WHERE title='Aenima'), 1),
('Eulogy', 'Dynamic and aggressive', 510, (SELECT id FROM "release" WHERE title='Aenima'), 1),
('Forty Six & 2', 'Iconic bass-driven track', 360, (SELECT id FROM "release" WHERE title='Aenima'), 1),
('Aenema', 'Satirical and heavy anthem', 400, (SELECT id FROM "release" WHERE title='Aenima'), 1),

-- 10,000 Days
('Vicarious', 'Explosive opening track', 430, (SELECT id FROM "release" WHERE title='10,000 Days'), 1),
('Jambi', 'Middle Eastern-inspired riffs', 445, (SELECT id FROM "release" WHERE title='10,000 Days'), 1),
('The Pot', 'Funky and heavy', 380, (SELECT id FROM "release" WHERE title='10,000 Days'), 1),
('Right in Two', 'Philosophical and epic', 520, (SELECT id FROM "release" WHERE title='10,000 Days'), 1),

-- Jane Doe
('Concubine', 'Explosive and chaotic opener', 79, (SELECT id FROM "release" WHERE title='Jane Doe'), 0),
('Fault and Fracture', 'Complex rhythms and aggression', 185, (SELECT id FROM "release" WHERE title='Jane Doe'), 0),
('Distance and Meaning', 'Heavy and brooding track', 258, (SELECT id FROM "release" WHERE title='Jane Doe'), 0),
('Hell to Pay', 'Dark and sludge-influenced', 272, (SELECT id FROM "release" WHERE title='Jane Doe'), 0),
('Homewrecker', 'Anthemic hardcore song', 231, (SELECT id FROM "release" WHERE title='Jane Doe'), 0),
('The Broken Vow', 'Fast-paced and intense', 133, (SELECT id FROM "release" WHERE title='Jane Doe'), 0),
('Bitter and Then Some', 'Relentless speed', 88, (SELECT id FROM "release" WHERE title='Jane Doe'), 0),
('Heaven in Her Arms', 'Emotional and melodic heavy track', 241, (SELECT id FROM "release" WHERE title='Jane Doe'), 0),
('Phoenix in Flight', 'Atmospheric and slow build', 229, (SELECT id FROM "release" WHERE title='Jane Doe'), 0),
('Phoenix in Flames', 'Short burst of noise', 42, (SELECT id FROM "release" WHERE title='Jane Doe'), 0),
('Thaw', 'Dissonant and heavy', 270, (SELECT id FROM "release" WHERE title='Jane Doe'), 0),
('Jane Doe', 'Epic and emotional title track', 694, (SELECT id FROM "release" WHERE title='Jane Doe'), 0);

COMMIT;
