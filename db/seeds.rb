# This file should contain all the record creation needed to seed the database with its default values.
# The data can then be loaded with the rails db:seed command (or created alongside the database with db:setup).

District.create(id: 1, name: 'Sunnyside School District', address: '120 Main St.', city: 'Columbus', state: 'OH', zip_code: '43214', phone_number: '6141231000')

schools = School.create([{id: 1, name: 'Sunnyside Elementary School', address: '121 Main St.', city: 'Columbus', state: 'OH', zip_code: '43214', phone_number: '6141232000', district_id: 1},
                        {id: 2, name: 'Sunnyside Middle School', address: '122 Main St.', city: 'Columbus', state: 'OH', zip_code: '43214', phone_number: '6141233000', district_id: 1},
                        {id: 3, name: 'Sunnyside High School', address: '123 Main St.', city: 'Columbus', state: 'OH', zip_code: '43214', phone_number: '6141234000', district_id: 1}])

SchoolYear.create(id: 1, school_id: 3, title: '2016-2017', short_name: 'FY', year: 2016, start_date: '9/6/2016', end_date: '5/26/2017', grade_start_date: '8/1/2016', grade_end_date: '5/31/2017', reg_start_date: '6/1/2015', reg_end_date: '12/31/2016')

Student.create(student_id: 1000, first_name: 'Justin', middle_name: 'M', last_name: 'Adkins', sex: 'M', date_of_birth: '07/18/1994')

grades = Grade.create([{ id: 1, title: 'Kindergarten', short_name: 'K', next_grade_id: 2 },
                       { id: 2, title: '1st Grade', short_name: '1', next_grade_id: 3, previous_grade_id: 1 },
                       { id: 3, title: '2nd Grade', short_name: '2', next_grade_id: 4, previous_grade_id: 2 },
                       { id: 4, title: '3rd Grade', short_name: '3', next_grade_id: 5, previous_grade_id: 3 },
                       { id: 5, title: '4th Grade', short_name: '4', next_grade_id: 6, previous_grade_id: 4 },
                       { id: 6, title: '5th Grade', short_name: '5', next_grade_id: 7, previous_grade_id: 5 },
                       { id: 7, title: '6th Grade', short_name: '6', next_grade_id: 8, previous_grade_id: 6 },
                       { id: 8, title: '7th Grade', short_name: '7', next_grade_id: 9, previous_grade_id: 7 },
                       { id: 9, title: '8th Grade', short_name: '8', next_grade_id: 10, previous_grade_id: 8 },
                       { id: 10, title: '9th Grade - Freshman', short_name: '9', next_grade_id: 11, previous_grade_id: 9 },
                       { id: 11, title: '10th Grade - Sophomore', short_name: '10', next_grade_id: 12, previous_grade_id: 10 },
                       { id: 12, title: '11th Grade - Junior', short_name: '11', next_grade_id: 13, previous_grade_id: 11 },
                       { id: 13, title: '12th Grade - Senior', short_name: '12', previous_grade_id: 12 }])

mark_scales = ReportCardGradeScale.create([{id: 1, name: '6-12 Scale', description: 'Default grading scale for middle and high schools.', is_active: true },
                                           { id: 2, name: 'K-5 Scale', description: 'Default grading scale for elementary schools.', is_active: true }])

marks = ReportCardGrade.create([{id: 1, title: 'A+', gpa_value: 4.000, breakoff: 99, weighted_gpa_scale: 4.000, mark_scale_id: 1 },
                                { id: 2, title: 'A', gpa_value: 4.000, breakoff: 93, weighted_gpa_scale: 4.000, mark_scale_id: 1 },
                                { id: 3, title: 'A-', gpa_value: 4.000, breakoff: 90, weighted_gpa_scale: 4.000, mark_scale_id: 1 },
                                { id: 4, title: 'B+', gpa_value: 3.000, breakoff: 89, weighted_gpa_scale: 3.000, mark_scale_id: 1 },
                                { id: 5, title: 'B', gpa_value: 3.000, breakoff: 83, weighted_gpa_scale: 3.000, mark_scale_id: 1 },
                                { id: 6, title: 'B-', gpa_value: 3.000, breakoff: 80, weighted_gpa_scale: 3.000, mark_scale_id: 1 },
                                { id: 7, title: 'C+', gpa_value: 2.000, breakoff: 79, weighted_gpa_scale: 2.000, mark_scale_id: 1 },
                                { id: 8, title: 'C', gpa_value: 2.000, breakoff: 73, weighted_gpa_scale: 2.000, mark_scale_id: 1 },
                                { id: 9, title: 'C-', gpa_value: 2.000, breakoff: 70, weighted_gpa_scale: 2.000, mark_scale_id: 1 },
                                { id: 10, title: 'D+', gpa_value: 1.000, breakoff: 69, weighted_gpa_scale: 1.000, mark_scale_id: 1 },
                                { id: 11, title: 'D', gpa_value: 1.000, breakoff: 63, weighted_gpa_scale: 1.000, mark_scale_id: 1 },
                                { id: 12, title: 'D-', gpa_value: 1.000, breakoff: 60, weighted_gpa_scale: 1.000, mark_scale_id: 1 },
                                { id: 13, title: 'F', gpa_value: 0.000, breakoff: 0, weighted_gpa_scale: 0, mark_scale_id: 1 },
                                { id: 14, title: 'I', description: 'Incomplete', gpa_value: 0, breakoff: 0, weighted_gpa_scale: 0, mark_scale_id: 1 },
                                { id: 15, title: 'N/A', description: 'Not Applicable', gpa_value: 0, breakoff: 0, weighted_gpa_scale: 0, mark_scale_id: 1 },
                                { id: 16, title: 'O', description: 'Outstanding', gpa_value: 4.000, breakoff: 90, weighted_gpa_scale: 4, mark_scale_id: 2 },
                                { id: 17, title: 'S', description: 'Satisfactory', gpa_value: 3.000, breakoff: 80, weighted_gpa_scale: 3, mark_scale_id: 2 },
                                { id: 18, title: 'I', description: 'Imporvement Needed', gpa_value: 2.000, breakoff: 70, weighted_gpa_scale: 2, mark_scale_id: 2 },
                                { id: 19, title: 'U', description: 'Unsatisfactory', gpa_value: 1.000, breakoff: 60, weighted_gpa_scale: 1, mark_scale_id: 2 },
                                { id: 20, title: 'F', description: 'Failed', gpa_value: 0.000, breakoff: 0, weighted_gpa_scale: 0, mark_scale_id: 2 }])

enrollment_codes = EnrollmentCode.create([{id: 1, title: 'Beginning of Year', short_name: 'STRT', is_admission: true, is_active: true},
                                          {id: 2, title: 'From Other District', short_name: 'OTHER', is_admission: true, is_active: true},
                                          {id: 3, title: 'Transferred in District', short_name: 'INTR', is_admission: true, is_active: true},
                                          {id: 4, title: 'Expelled', short_name: 'EXP', is_admission: false, is_active: true},
                                          {id: 5, title: 'Moved from District', short_name: 'MOVE', is_admission: false, is_active: true},
                                          {id: 6, title: 'Transferred in District', short_name: 'TRAN', is_admission: false, is_active: true}])